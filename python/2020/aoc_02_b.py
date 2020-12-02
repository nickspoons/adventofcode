""" Advent of Code, 2020: Day 02, b """

import re

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)


def run():
    """ Use regex to extract arguments from line, then check password """
    valid = 0
    for line in inputs:
        match = re.match(r"(\d+)-(\d+) (.): (.*)", line)
        pos1 = int(match.group(1))
        pos2 = int(match.group(2))
        char = match.group(3)
        password = match.group(4)
        val1 = password[pos1 - 1] == char
        val2 = password[pos2 - 1] == char
        if int(val1) + int(val2) == 1:
            valid += 1

    return valid


if __name__ == "__main__":
    print(run())
