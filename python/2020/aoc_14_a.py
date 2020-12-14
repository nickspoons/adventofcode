""" Advent of Code, 2020: Day 14, a """

import re

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Apply bitmasks to values and save the results in a dictionary """
    memory = dict()
    for line in inputs:
        if line[:4] == "mask":
            omask = int(line[7:].replace("X", "0"), 2)
            amask = int(line[7:].replace("X", "1"), 2)
        else:
            match = re.match(r"mem\[(\d+)\] = (\d+)$", line)
            value = (int(match.group(2)) | omask) & amask
            memory[int(match.group(1))] = value
    return sum(memory.values())


if __name__ == "__main__":
    print(run())
