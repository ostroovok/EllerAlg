﻿using System.Collections;
using System;

namespace EllerAlg
{
	public class NewCell
	{
		public bool up = true;
		public bool down = true;
		public bool left = true;
		public bool right = true;
	}

	public class Maze
	{
		public readonly int width;
		public readonly int height;

		public readonly NewCell[] cells;

		public Maze(int w, int h)
		{
			width = w;
			height = h;

			cells = new NewCell[width * height];

			for (int i = 0; i < cells.Length; i++)
			{
				cells[i] = new NewCell();
			}
		}

		public NewCell at(int r, int c)
		{
			return cells[r * width + c];
		}
	}

	public class Eller
	{
		public static Maze Generate(int width, int height)
		{
			var random = new Random();

			var maze = new Maze(width, height);

			// For a set of cells i_1, i_2, ..., i_k from left to right that are
			// connected in the previous row R[i_1] = i_2, R[i_2] = i_3, ... and
			// R[i_k] = i_1. Similarly for the left
			var L = new int[width];
			var R = new int[width];

			// At the top each cell is connected only to itself
			for (var c = 0; c < width; c++)
			{
				L[c] = c;
				R[c] = c;
			}

			// Generate each row of the maze excluding the last
			for (var r = 0; r < height - 1; r++)
			{
				for (var c = 0; c < width; c++)
				{
					// Should we connect this cell and its neighbour to the right?
					if (c != width - 1 && c + 1 != R[c] && random.NextDouble() < 0.5)
					{
						R[L[c + 1]] = R[c]; // Link L[c+1] to R[c]
						L[R[c]] = L[c + 1];
						R[c] = c + 1; // Link c to c+1
						L[c + 1] = c;

						maze.at(r, c).right = false;
						maze.at(r, c + 1).left = false;
					}

					// Should we connect this cell and its neighbour below?
					if (c != R[c] && random.NextDouble() < 0.5)
					{
						R[L[c]] = R[c]; // Link L[c] to R[c]
						L[R[c]] = L[c];
						R[c] = c; // Link c to c
						L[c] = c;
					}
					else
					{
						maze.at(r, c).down = false;
						maze.at(r + 1, c).up = false;
					}
				}
			}

			// Handle the last row to guarantee the maze is connected
			for (var c = 0; c < width; c++)
			{
				if (c != width - 1 && c + 1 != R[c] && (c == R[c] || random.NextDouble() < 0.5))
				{
					R[L[c + 1]] = R[c]; // Link L[c+1] to R[c]
					L[R[c]] = L[c + 1];
					R[c] = c + 1; // Link c to c+1
					L[c + 1] = c;

					maze.at(height - 1, c).right = false;
					maze.at(height - 1, c + 1).left = false;
				}

				R[L[c]] = R[c]; // Link L[c] to R[c]
				L[R[c]] = L[c];
				R[c] = c; // Link c to c
				L[c] = c;
			}

			// Entrance and exit
			maze.at(0, 0).left = false;
			maze.at(height - 1, width - 1).right = false;

			return maze;
		}
	}
}