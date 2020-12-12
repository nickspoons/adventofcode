""" Advent of Code, 2020: Day 12, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Move ship in given directions """
    directions = [[line[0], int(line[1:])] for line in inputs]
    points = ["E", "S", "W", "N"]
    x, y, facing = 0, 0, 0
    for direction in directions:
        command = direction[0]
        amount = direction[1]
        if command == "F":
            command = points[facing]
        if command == "N":
            y += amount
        elif command == "S":
            y -= amount
        elif command == "W":
            x -= amount
        elif command == "E":
            x += amount
        else:
            rotate = (amount // 90) % 4
            if command == "L":
                rotate = 0 - rotate
            facing += rotate
            if facing < 0:
                facing += 4
            if facing >= 4:
                facing -= 4
    return abs(x) + abs(y)


if __name__ == "__main__":
    print(run())
