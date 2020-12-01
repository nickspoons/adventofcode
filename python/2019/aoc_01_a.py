""" Advent of Code, 2019: Day 01, a """

# __file__ contains this file's filename, e.g. aoc_01_a.py when run from this
# directory or C:\code\aoc\python\2019\aoc_01_a.py when run from elsewhere.
# We want to replace the "_a.py" at the end with "_input" to get the correct
# input file name, e.g. aoc_01_input
input_file = __file__[:-5] + "_input"

# Open the input file
with open(input_file) as f:
    # Read the inputs line by line into variable "inputs"
    inputs = list(f)

# "inputs" now contains a list of "strings", one for each line of the input.
# Convert these to integers, so they can be manipulated mathematically
input_ints = [int(line) for line in inputs]


def run():
    # Do some maths on each line, and sum all the results into variable "fuel"
    fuel = sum(line // 3 - 2 for line in input_ints)

    return fuel


if __name__ == "__main__":
    print(run())
