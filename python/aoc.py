#!/usr/bin/env python3

"""
Run python advent of code programs.

Usage:  ./aoc.py <year> <day> [b]

  eg.   ./aoc.py 2019 1
        ./aoc.py 2019 1 b
"""

import importlib
import sys
import time


def run(year, day, a_or_b):
    """Load and run the requested program"""
    filename = f"{year}.aoc_{day:02d}_{'b' if a_or_b else 'a'}"

    mod = importlib.import_module(filename, f"{year}")
    start = time.time()
    result = mod.run()
    end = time.time()

    print(f"{result:<20} in {end - start:0.5f}s")


if __name__ == "__main__":
    if len(sys.argv) < 3:
        print(__doc__)
        sys.exit(0)

    if not sys.argv[1].isnumeric() or not sys.argv[2].isnumeric():
        sys.exit("Invalid argument type")

    arg_year = int(sys.argv[1])
    arg_day = int(sys.argv[2])
    arg_a = len(sys.argv) > 3

    run(arg_year, arg_day, arg_a)
