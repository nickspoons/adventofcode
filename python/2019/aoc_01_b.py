""" Advent of Code, 2019: Day 01, b """

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)
input_ints = [int(line) for line in inputs]


def run():
    fuel = 0
    for mod in input_ints:
        mod_fuel, weight = 0, mod
        while True:
            weight = weight // 3 - 2
            if weight <= 0:
                break
            mod_fuel += weight
        fuel += mod_fuel

    return fuel


if __name__ == "__main__":
    print(run())
