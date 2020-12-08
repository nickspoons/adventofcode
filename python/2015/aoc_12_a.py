""" Advent of Code, 2015: Day 12, a """

import json

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def find_numbers(data):
    """ Recursively find numbers in JSON data """
    if isinstance(data, int):
        return [data]
    numbers = []
    if isinstance(data, list):
        for dat in data:
            numbers.extend(find_numbers(dat))
    elif isinstance(data, dict):
        for key in data:
            if isinstance(key, int):
                numbers.append(key)
            numbers.extend(find_numbers(data[key]))
    return numbers


def run():
    """ Load JSON data and sum all numbers in it """
    return sum(find_numbers(json.loads(inputs[0])))


if __name__ == "__main__":
    print(run())
