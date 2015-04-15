﻿using System.Linq;
using NUnit.Framework;

namespace CoreLib.Tests.RuntimeLibraryTests {
	[TestFixture]
	public class NumericConversionTests {
		[Test]
		public void UncheckedTruncatingIntegerConversions() {
			SourceVerifier.AssertSourceCorrect(@"
class C {
	void M() {
		ulong src = 0;
		long src2 = 0;

		unchecked {
			// BEGIN
			var sb1 = (sbyte)src;
			var b1  = (byte)src;
			var s1  = (short)src;
			var us1 = (ushort)src;
			var i1  = (int)src;
			var ui1 = (uint)src;
			var l1  = (long)src;
			var ul1 = (ulong)src2;
			var c1  = (char)src;
			// END
		}
	}
}",
@"				var sb1 = ss.sxb(src & 255);
				var b1 = src & 255;
				var s1 = ss.sxs(src & 65535);
				var us1 = src & 65535;
				var i1 = src | 0;
				var ui1 = src >>> 0;
				var l1 = ss.clip64(src);
				var ul1 = ss.clipu64(src2);
				var c1 = src & 65535;
");
		}

		[Test]
		public void UncheckedFloatToIntConversions() {
			SourceVerifier.AssertSourceCorrect(@"
class C {
	void M() {
		double src = 0;

		unchecked {
			// BEGIN
			var sb1 = (sbyte)src;
			var b1  = (byte)src;
			var s1  = (short)src;
			var us1 = (ushort)src;
			var i1  = (int)src;
			var ui1 = (uint)src;
			var l1  = (long)src;
			var ul1 = (ulong)src;
			var c1  = (char)src;
			// END
		}
	}
}",
@"				var sb1 = ss.sxb(src & 255);
				var b1 = src & 255;
				var s1 = ss.sxs(src & 65535);
				var us1 = src & 65535;
				var i1 = src | 0;
				var ui1 = src >>> 0;
				var l1 = ss.clip64(src);
				var ul1 = ss.clipu64(src);
				var c1 = src & 65535;
");
		}

		[Test]
		public void UncheckedLiftedIntegerConversions() {
			SourceVerifier.AssertSourceCorrect(@"
class C {
	void M() {
		ulong? src = 0;
		long? src2 = 0;

		unchecked {
			// BEGIN
			var sb1 = (sbyte?)src;
			var b1  = (byte?)src;
			var s1  = (short?)src;
			var us1 = (ushort?)src;
			var i1  = (int?)src;
			var ui1 = (uint?)src;
			var l1  = (long?)src;
			var ul1 = (ulong?)src2;
			var c1  = (char?)src;
			// END
		}
	}
}",
@"				var sb1 = ss.clip8(src);
				var b1 = ss.clipu8(src);
				var s1 = ss.clip16(src);
				var us1 = ss.clipu16(src);
				var i1 = ss.clip32(src);
				var ui1 = ss.clipu32(src);
				var l1 = ss.clip64(src);
				var ul1 = ss.clipu64(src2);
				var c1 = ss.clipu16(src);
");
		}

		[Test]
		public void UncheckedLiftedFloatToIntConversions() {
			SourceVerifier.AssertSourceCorrect(@"
class C {
	void M() {
		double? src = 0;

		unchecked {
			// BEGIN
			var sb1 = (sbyte?)src;
			var b1  = (byte?)src;
			var s1  = (short?)src;
			var us1 = (ushort?)src;
			var i1  = (int?)src;
			var ui1 = (uint?)src;
			var l1  = (long?)src;
			var ul1 = (ulong?)src;
			var c1  = (char?)src;
			// END
		}
	}
}",
@"				var sb1 = ss.clip8(src);
				var b1 = ss.clipu8(src);
				var s1 = ss.clip16(src);
				var us1 = ss.clipu16(src);
				var i1 = ss.clip32(src);
				var ui1 = ss.clipu32(src);
				var l1 = ss.clip64(src);
				var ul1 = ss.clipu64(src);
				var c1 = ss.clipu16(src);
");
		}

		[Test]
		public void CheckedTruncatingIntegerConversions() {
			SourceVerifier.AssertSourceCorrect(@"
class C {
	void M() {
		ulong src = 0;
		long src2 = 0;

		checked {
			// BEGIN
			var sb1 = (sbyte)src;
			var b1  = (byte)src;
			var s1  = (short)src;
			var us1 = (ushort)src;
			var i1  = (int)src;
			var ui1 = (uint)src;
			var l1  = (long)src;
			var ul1 = (ulong)src2;
			var c1  = (char)src;
			// END
		}
	}
}",
@"				var sb1 = ss.ck(src, ss.SByte);
				var b1 = ss.ck(src, ss.Byte);
				var s1 = ss.ck(src, ss.Int16);
				var us1 = ss.ck(src, ss.UInt16);
				var i1 = ss.ck(src, ss.Int32);
				var ui1 = ss.ck(src, ss.UInt32);
				var l1 = ss.ck(src, ss.Int64);
				var ul1 = ss.ck(src2, ss.UInt64);
				var c1 = ss.ck(src, ss.Char);
");
		}

		[Test]
		public void CheckedFloatToIntConversions() {
			SourceVerifier.AssertSourceCorrect(@"
class C {
	void M() {
		double src = 0;

		checked {
			// BEGIN
			var sb1 = (sbyte?)src;
			var b1  = (byte?)src;
			var s1  = (short?)src;
			var us1 = (ushort?)src;
			var i1  = (int?)src;
			var ui1 = (uint?)src;
			var l1  = (long?)src;
			var ul1 = (ulong?)src;
			var c1  = (char?)src;
			// END
		}
	}
}",
@"				var sb1 = ss.ck(ss.trunc(src), ss.SByte);
				var b1 = ss.ck(ss.trunc(src), ss.Byte);
				var s1 = ss.ck(ss.trunc(src), ss.Int16);
				var us1 = ss.ck(ss.trunc(src), ss.UInt16);
				var i1 = ss.ck(ss.trunc(src), ss.Int32);
				var ui1 = ss.ck(ss.trunc(src), ss.UInt32);
				var l1 = ss.ck(ss.trunc(src), ss.Int64);
				var ul1 = ss.ck(ss.trunc(src), ss.UInt64);
				var c1 = ss.ck(ss.trunc(src), ss.Char);
");
		}

		[Test]
		public void CheckedLiftedIntegerConversions() {
			SourceVerifier.AssertSourceCorrect(@"
class C {
	void M() {
		ulong? src = 0;
		long? src2 = 0;

		checked {
			// BEGIN
			var sb1 = (sbyte?)src;
			var b1  = (byte?)src;
			var s1  = (short?)src;
			var us1 = (ushort?)src;
			var i1  = (int?)src;
			var ui1 = (uint?)src;
			var l1  = (long?)src;
			var ul1 = (ulong?)src2;
			var c1  = (char?)src;
			// END
		}
	}
}",
@"				var sb1 = ss.ck(src, ss.SByte);
				var b1 = ss.ck(src, ss.Byte);
				var s1 = ss.ck(src, ss.Int16);
				var us1 = ss.ck(src, ss.UInt16);
				var i1 = ss.ck(src, ss.Int32);
				var ui1 = ss.ck(src, ss.UInt32);
				var l1 = ss.ck(src, ss.Int64);
				var ul1 = ss.ck(src2, ss.UInt64);
				var c1 = ss.ck(src, ss.Char);
");
		}

		[Test]
		public void CheckedLiftedFloatToIntConversions() {
			SourceVerifier.AssertSourceCorrect(@"
class C {
	void M() {
		double? src = 0;

		checked {
			// BEGIN
			var sb1 = (sbyte?)src;
			var b1  = (byte?)src;
			var s1  = (short?)src;
			var us1 = (ushort?)src;
			var i1  = (int?)src;
			var ui1 = (uint?)src;
			var l1  = (long?)src;
			var ul1 = (ulong?)src;
			var c1  = (char?)src;
			// END
		}
	}
}",
@"				var sb1 = ss.ck(ss.trunc(src), ss.SByte);
				var b1 = ss.ck(ss.trunc(src), ss.Byte);
				var s1 = ss.ck(ss.trunc(src), ss.Int16);
				var us1 = ss.ck(ss.trunc(src), ss.UInt16);
				var i1 = ss.ck(ss.trunc(src), ss.Int32);
				var ui1 = ss.ck(ss.trunc(src), ss.UInt32);
				var l1 = ss.ck(ss.trunc(src), ss.Int64);
				var ul1 = ss.ck(ss.trunc(src), ss.UInt64);
				var c1 = ss.ck(ss.trunc(src), ss.Char);
");
		}

		[Test]
		public void ImplicitClips() {
			SourceVerifier.AssertSourceCorrect(@"
class C {
	void M() {
		sbyte  sb = 0;
		byte   b  = 0;
		short  s  = 0;
		ushort us = 0;
		int    i  = 0;
		uint   ui = 0;
		long   l  = 0;
		ulong  ul = 0;
		char   c  = '0';

		unchecked {
			// BEGIN
			sb += 100;
			b  += 100;
			s  += 100;
			us += 100;
			i  += 100;
			ui += 100;
			l  += 100;
			ul += 100;
			c  += '0';
			// END
		}
	}
}",
@"				sb = ss.sxb(sb + 100 & 255);
				b = b + 100 & 255;
				s = ss.sxs(s + 100 & 65535);
				us = us + 100 & 65535;
				i += 100;
				ui = ui + 100 >>> 0;
				l += 100;
				ul = ss.clipu64(ul + 100);
				c = c + 48 & 65535;
");
		}

		[Test]
		public void ImplicitClipsNullable() {
			SourceVerifier.AssertSourceCorrect(@"
class C {
	void M() {
		sbyte?  sb = 0;
		byte?   b  = 0;
		short?  s  = 0;
		ushort? us = 0;
		int?    i  = 0;
		uint?   ui = 0;
		long?   l  = 0;
		ulong?  ul = 0;
		char?   c  = '0';

		unchecked {
			// BEGIN
			sb += 100;
			b  += 100;
			s  += 100;
			us += 100;
			i  += 100;
			ui += 100;
			l  += 100;
			ul += 100;
			c  += '0';
			// END
		}
	}
}",
@"				sb = ss.clip8(ss.Nullable$1.add(sb, 100));
				b = ss.clipu8(ss.Nullable$1.add(b, 100));
				s = ss.clip16(ss.Nullable$1.add(s, 100));
				us = ss.clipu16(ss.Nullable$1.add(us, 100));
				i = ss.Nullable$1.add(i, 100);
				ui = ss.clipu32(ss.Nullable$1.add(ui, 100));
				l = ss.Nullable$1.add(l, 100);
				ul = ss.clipu64(ss.Nullable$1.add(ul, 100));
				c = ss.clipu16(ss.Nullable$1.add(c, 48));
");
		}
	}
}