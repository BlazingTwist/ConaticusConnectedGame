using System;

namespace DataTypes {

	[Flags]
	public enum ProductColor : int {

		Black = 0x000000,
		Red = 0xff0000,
		Green = 0x00ff00,
		Blue = 0x0000ff,
		Yellow = 0xffff00,
		Magenta = 0xff00ff,
		Cyan = 0x00ffff,
		White = 0xffffff

	}

}
