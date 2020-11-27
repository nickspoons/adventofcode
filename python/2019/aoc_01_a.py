""" Advent of Code, 2019: Day 01, a """

import os.path

input_file = os.path.join(os.path.dirname(__file__), "aoc_01_input")

# Open the input file
with open(input_file) as f:
    # Read the inputs line by line into variable "inputs"
    inputs = list(f)

# "inputs" now contains a list of "strings", one for each line of the input.
# Convert these to integers, so they can be manipulated mathematically
input_ints = [int(line) for line in inputs]

# Do some maths on each line, and sum all the results into variable "fuel"
fuel = sum(line // 3 - 2 for line in input_ints)

print(fuel)
