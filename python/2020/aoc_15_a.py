""" Advent of Code, 2020: Day 15, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Count back from each number to find its last occurrence """
    numbers = [int(n) for n in inputs[0].split(",")]
    index, target = len(numbers) - 1, 2020
    while True:
        age = 0
        for i in range(index - 1, -1, -1):
            if numbers[i] == numbers[index]:
                age = index - i
                break
        numbers.append(age)
        if index == target:
            break
        index += 1
    return numbers[target - 1]


if __name__ == "__main__":
    print(run())
