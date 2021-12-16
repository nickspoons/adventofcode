using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdventOfCode.Advent2021 {
   public class Day16 : AdventDay {
      public override int Day => 16;
      public override int Year => 2021;

      private string Binary { get; set; }
      private int Index { get; set; }

      private void ParseInput() {
         string hex = InputLines[0];
         string binary = "";
         foreach (char c in hex) {
            int n = int.Parse(c.ToString(), NumberStyles.HexNumber);
            string binchunk = Convert.ToString(n, 2);
            binary += $"{new string('0', 4 - binchunk.Length)}{binchunk}";
         }
         Binary = binary;
      }

      private int ReadBinaryInt(int length) {
         Index += length;
         return Convert.ToInt32(Binary.Substring(Index - length, length), 2);
      }

      private long ReadLiteral() {
         bool done = false;
         string part = "";
         while (!done) {
            done = Binary[Index] == '0';
            part += Binary.Substring(Index + 1, 4);
            Index += 5;
         }
         return Convert.ToInt64(part, 2);
      }

      private IEnumerable<Packet> ReadOperator() {
         char mode = Binary[Index++];
         if (mode == '0') {
            int length = ReadBinaryInt(15);
            int end = Index + length;
            while (Index < end)
               yield return ReadPacket();
         }
         else {
            int numPackets = ReadBinaryInt(11);
            for (int n = 0; n < numPackets; n++)
               yield return ReadPacket();
         }
      }

      private Packet ReadPacket() {
         int version = ReadBinaryInt(3);
         int type = ReadBinaryInt(3);
         if (type == 4)
            return new LiteralPacket(version, type, ReadLiteral());
         else
            return new OperatorPacket(version, type, ReadOperator().ToArray());
      }

      public override string A() {
         ParseInput();
         return ReadPacket().SumVersions.ToString();
      }

      public override string B() {
         ParseInput();
         return ReadPacket().Value.ToString();
      }

      private abstract class Packet {
         public Packet(int version, int type) {
            Type = type;
            Version = version;
         }

         public int Type { get; }
         public int Version { get; }

         public abstract long Value { get; }
         public abstract int SumVersions { get; }
      }

      private class LiteralPacket : Packet {
         public LiteralPacket(int version, int type, long value) : base(version, type) {
            Value = value;
         }

         public override long Value { get; }

         public override int SumVersions => Version;
      }

      private class OperatorPacket : Packet {
         public OperatorPacket(int version, int type, Packet[] packets) : base(version, type) {
            Packets = packets;
         }

         public Packet[] Packets { get; }

         public override int SumVersions => Packets.Sum(p => p.SumVersions) + Version;

         public override long Value {
            get {
               switch (Type) {
                  case 0:
                     return Packets.Sum(p => p.Value);
                  case 1:
                     return Packets.Aggregate(1L, (acc, val) => acc * val.Value);
                  case 2:
                     return Packets.Min(p => p.Value);
                  case 3:
                     return Packets.Max(p => p.Value);
                  case 5:
                     return Packets[0].Value > Packets[1].Value ? 1 : 0;
                  case 6:
                     return Packets[0].Value < Packets[1].Value ? 1 : 0;
                  case 7:
                     return Packets[0].Value == Packets[1].Value ? 1 : 0;
                  default:
                     return 0;
               }
            }
         }
      }
   }
}
