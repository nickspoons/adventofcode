""" Advent of Code, 2019: Day 01, b """

input_file = __file__[:-5] + "_input"
with open(input_file) as f:
    # Read the inputs line by line into variable "inputs"
    inputs = list(f)

# "inputs" now contains a list of "strings", one for each line of the input.
# Convert these to integers, so they can be manipulated mathematically
input_ints = [int(line) for line in inputs]

fuel = 0
for mod in input_ints:
    mod_fuel, weight = 0, mod
    while True:
        weight = weight // 3 - 2
        if weight <= 0:
            break
        mod_fuel += weight
    fuel += mod_fuel

print(fuel)
