""" Advent of Code, 2015: Day 06, a """

import re

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Turn lights on/off following various instructions """
    grid = []
    for y in range(1000):
        grid.append(1000 * [0])
    pattern = r"(turn on|turn off|toggle) (\d+),(\d+) through (\d+),(\d+)"
    for instruction in inputs:
        match = re.match(pattern, instruction)
        for y in range(int(match.group(3)), int(match.group(5)) + 1):
            for x in range(int(match.group(2)), int(match.group(4)) + 1):
                if match.group(1) == "turn on":
                    grid[y][x] = 1
                elif match.group(1) == "turn off":
                    grid[y][x] = 0
                else:
                    grid[y][x] = 1 - grid[y][x]

    return sum([sum(row) for row in grid])


if __name__ == "__main__":
    print(run())
