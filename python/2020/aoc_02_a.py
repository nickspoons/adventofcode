""" Advent of Code, 2020: Day 02, a """

import re

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)


def run():
    """ Use regex to extract arguments from line, then check password """
    valid = 0
    for line in inputs:
        match = re.match(r"(\d+)-(\d+) (.): (.*)", line)
        limit_min = int(match.group(1))
        limit_max = int(match.group(2))
        char = match.group(3)
        password = match.group(4)
        num = password.count(char)
        if limit_min <= num <= limit_max:
            valid += 1

    return valid


if __name__ == "__main__":
    print(run())
