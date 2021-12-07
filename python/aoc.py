#!/usr/bin/env python3

"""
Run python advent of code programs.

Usage:  ./aoc.py <year> <day> [b]

  eg.   ./aoc.py 2019 1
        ./aoc.py 2019 1 b

Download the day's input file:
        ./aoc.py 2020 5 input

Note: downloading the input file requires a valid session cookie, which can be
fetched from browser developer tools.
"""

import importlib
import sys
import time

import requests


def run(year, day, a_or_b):
    """ Load and run the requested program """
    filename = f"{year}.aoc_{day:02d}_{a_or_b}"

    mod = importlib.import_module(filename, f"{year}")
    start = time.time()
    result = mod.run()
    end = time.time()

    print(f"{result:<20} in {end - start:0.5f}s")


def download_input(year, day):
    """ Request the day's input file from https://adventofcode.com """
    with open("session_cookie") as f:
        session_cookie = f.readline().strip()

    try:
        url = f"https://adventofcode.com/{year}/day/{day}/input"
        cookies = {"session": session_cookie}
        req = requests.get(url, cookies=cookies)
        req.raise_for_status()
    except requests.exceptions.RequestException as err:
        if err.response.status_code == 400:
            print(err.response.text.strip())

    filename = f"{year}/aoc_{day:02d}_input"
    with open(filename, "w") as f:
        f.write(req.text)


if __name__ == "__main__":
    if len(sys.argv) < 3:
        print(__doc__)
        sys.exit(0)

    if not sys.argv[1].isnumeric() or not sys.argv[2].isnumeric():
        sys.exit("Invalid argument type")

    arg_year = int(sys.argv[1])
    arg_day = int(sys.argv[2])
    arg_part = ""
    if len(sys.argv) > 3:
        arg_part = sys.argv[3]

    if arg_part == "input":
        download_input(arg_year, arg_day)
    elif arg_part in ["a", "b"]:
        run(arg_year, arg_day, arg_part)
    else:
        print(f"Invalid part: {arg_part}")
