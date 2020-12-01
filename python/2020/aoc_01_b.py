""" Advent of Code, 2020: Day 01, b """

import sys

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)
input_ints = [int(line) for line in inputs]

result = 0
for val1 in input_ints:
    for val2 in input_ints:
        for val3 in input_ints:
            if val1 + val2 + val3 == 2020:
                print(val1 * val2 * val3)
                sys.exit()
