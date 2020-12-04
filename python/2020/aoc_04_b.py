""" Advent of Code, 2020: Day 04, b """

import re

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)


def validate_byr(field):
    """
    byr (Birth Year) - four digits; at least 1920 and at most 2002.
    """
    return field.isdigit() and 1920 <= int(field) <= 2002


def validate_iyr(field):
    """
    iyr (Issue Year) - four digits; at least 2010 and at most 2020.
    """
    return field.isdigit() and 2010 <= int(field) <= 2020


def validate_eyr(field):
    """
    eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
    """
    return field.isdigit() and 2020 <= int(field) <= 2030


def validate_hgt(field):
    """
    hgt (Height) - a number followed by either cm or in:
    If cm, the number must be at least 150 and at most 193.
    If in, the number must be at least 59 and at most 76.
    """
    match = re.match(r"^(\d+)(cm|in)$", field)
    if not match:
        return False
    if match.group(2) == "cm":
        return 150 <= int(match.group(1)) <= 193
    return 59 <= int(match.group(1)) <= 76


def validate_hcl(field):
    """
    hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
    """
    return re.match(r"^#[0-9a-f]{6}$", field)


def validate_ecl(field):
    """
    ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
    """
    return field in ['amb', 'blu', 'brn', 'gry', 'grn', 'hzl', 'oth']


def validate_pid(field):
    """
    pid (Passport ID) - a nine-digit number, including leading zeroes.
    """
    return re.match(r"^[0-9]{9}$", field)


def run():
    """ Validate passport fields in various ways """
    passports = []
    passport = []
    for line in inputs:
        if line.strip() == "":
            passports.append(passport)
            passport = []
        else:
            passport += line.strip().split(' ')
    passports.append(passport)

    valid = 0
    for passport in passports:
        fields = {f.split(':')[0]: f.split(':')[1] for f in passport}
        if 'cid' in fields:
            fields.pop('cid')
        if len(fields) == 7 \
                and validate_byr(fields['byr']) \
                and validate_iyr(fields['iyr']) \
                and validate_eyr(fields['eyr']) \
                and validate_hgt(fields['hgt']) \
                and validate_hcl(fields['hcl']) \
                and validate_ecl(fields['ecl']) \
                and validate_pid(fields['pid']):
            valid += 1

    return valid


if __name__ == "__main__":
    print(run())
