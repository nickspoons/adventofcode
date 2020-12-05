""" Advent of Code, 2015: Day 08, a """

import re

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Find the length difference between the strings and their values """
    total = 0
    for string in inputs:
        s = string[1:-1]
        s = re.sub(r"\\\\", "s", s)
        s = re.sub(r"\\\"", "q", s)
        s = re.sub(r"\\x..", "x", s)
        total += len(string) - len(s)
    return total


if __name__ == "__main__":
    print(run())
