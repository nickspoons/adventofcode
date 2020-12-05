""" Advent of Code, 2015: Day 08, b """

import re

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Find the length difference between the strings and their escapes """
    total = 0
    for string in inputs:
        s = re.sub(r"^\"", "QQQ", string)
        s = re.sub(r"\"$", "QQQ", s)
        s = re.sub(r"\"", "XX", s)
        s = re.sub(r"\\", "SS", s)
        total += len(s) - len(string)
    return total


if __name__ == "__main__":
    print(run())
