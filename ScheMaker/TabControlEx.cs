﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheMaker
{
	public class TabControlEx : TabControl
	{
		private Color bColor = Color.FromArgb(63, 63, 70);
		private Color tColor = Color.FromArgb(0, 122, 204);
		public TabControlEx()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
			UpdateStyles();
			KeyDown += TabControlEx_KeyDown;
		}

		private void TabControlEx_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.Modifiers & Keys.Control) == Keys.Control)
			{
				if (SelectedIndex >= 0)
				{
					if (e.KeyCode == Keys.W)
					{
						for (int i = 0; i < MainForm.Datas.Count; i++)
						{
							var s = MainForm.Datas.ElementAt(i);
							if (s.Value == TabPages[SelectedIndex])
							{
								MainForm.Datas.Remove(s.Key);
								break;
							}
						}
						TabPages.RemoveAt(SelectedIndex);
					}
					else if (e.KeyCode == Keys.S)
					{
						(Parent as MainForm).Save();
					}
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{

			if (TabCount > 0)
			{
				e.Graphics.FillRectangle(new SolidBrush(bColor), new RectangleF(0, 0, Width, Height));
				for (int i = 0; i < TabCount; i++)
				{
					Rectangle bounds = GetTabRect(i);
					if (SelectedIndex == i) e.Graphics.FillRectangle(new SolidBrush(tColor), GetTabRect(i));
					SizeF textSize = TextRenderer.MeasureText(TabPages[i].Text, Font);
					PointF textPoint = new PointF(bounds.X + bounds.Width / 2 - textSize.Width / 2, bounds.Y + bounds.Height / 2 - textSize.Height / 2);

					GetTabRect(i);
					e.Graphics.DrawString(TabPages[i].Text, Font, Brushes.White, textPoint);
				}
				e.Graphics.DrawLine(new Pen(tColor, 3), new Point(0, 23), new Point(Width, 23));
			}
			else
			{
				base.OnPaint(e);
				e.Graphics.FillRectangle(new SolidBrush(Parent.BackColor), new RectangleF(0, 0, Width, Height));
			}
		}
	}
}
