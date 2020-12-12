""" Advent of Code, 2020: Day 12, b """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Move waypoint for the ship to follow in given directions """
    directions = [[line[0], int(line[1:])] for line in inputs]
    x, y, waypointx, waypointy = 0, 0, 10, 1
    for direction in directions:
        command = direction[0]
        amount = direction[1]
        if command == "F":
            x += waypointx * amount
            y += waypointy * amount
        elif command == "N":
            waypointy += amount
        elif command == "S":
            waypointy -= amount
        elif command == "W":
            waypointx -= amount
        elif command == "E":
            waypointx += amount
        else:
            if amount == 180:
                waypointx = 0 - waypointx
                waypointy = 0 - waypointy
            else:
                # Rotate waypoint 90 degrees left
                waypointx, waypointy = 0 - waypointy, waypointx
                # If it should have gone right, flip it
                if (command == "L" and amount == 270) \
                        or (command == "R" and amount == 90):
                    waypointx = 0 - waypointx
                    waypointy = 0 - waypointy
    return abs(x) + abs(y)


if __name__ == "__main__":
    print(run())
