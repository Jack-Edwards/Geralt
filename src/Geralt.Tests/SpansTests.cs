using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geralt.Tests;

[TestClass]
public class SpansTests
{
    private static readonly byte[] Array1 = {0x00, 0x01, 0x02, 0x03};
    private static readonly byte[] Array2 = {0x04, 0x05, 0x06, 0x07};
    private static readonly byte[] Array3 = {0x08, 0x09, 0x10, 0x11};
    private static readonly byte[] Array4 = {0x12, 0x13, 0x14, 0x15};
    private static readonly byte[] Array5 = {0x16, 0x17, 0x18, 0x19};
    private static readonly byte[] Array6 = {0x20, 0x21, 0x22, 0x23};

    private static T[] Concat<T>(params T[][] arrays)
    {
        int offset = 0;
        var result = new T[arrays.Sum(array => array.Length)];
        foreach (var array in arrays)
        {
            Array.Copy(array, sourceIndex: 0, result, offset, array.Length);
            offset += array.Length;
        }
        return result;
    }
    
    [TestMethod]
    public void Concat_TwoSpans()
    {
        Span<byte> concatenated = stackalloc byte[Array1.Length + Array2.Length];
        Spans.Concat(concatenated, Array1, Array2);
        Span<byte> expected = Concat(Array1, Array2);
        Assert.IsTrue(concatenated.SequenceEqual(expected));
    }
    
    [TestMethod]
    public void Concat_ThreeSpans()
    {
        Span<byte> concatenated = stackalloc byte[Array1.Length + Array2.Length + Array3.Length];
        Spans.Concat(concatenated, Array1, Array2, Array3);
        Span<byte> expected = Concat(Array1, Array2, Array3);
        Assert.IsTrue(concatenated.SequenceEqual(expected));
    }
    
    [TestMethod]
    public void Concat_FourSpans()
    {
        Span<byte> concatenated = stackalloc byte[Array1.Length + Array2.Length + Array3.Length + Array4.Length];
        Spans.Concat(concatenated, Array1, Array2, Array3, Array4);
        Span<byte> expected = Concat(Array1, Array2, Array3, Array4);
        Assert.IsTrue(concatenated.SequenceEqual(expected));
    }
    
    [TestMethod]
    public void Concat_FiveSpans()
    {
        Span<byte> concatenated = stackalloc byte[Array1.Length + Array2.Length + Array3.Length + Array4.Length + Array5.Length];
        Spans.Concat(concatenated, Array1, Array2, Array3, Array4, Array5);
        Span<byte> expected = Concat(Array1, Array2, Array3, Array4, Array5);
        Assert.IsTrue(concatenated.SequenceEqual(expected));
    }
    
    [TestMethod]
    public void Concat_SixSpans()
    {
        Span<byte> concatenated = stackalloc byte[Array1.Length + Array2.Length + Array3.Length + Array4.Length + Array5.Length + Array6.Length];
        Spans.Concat(concatenated, Array1, Array2, Array3, Array4, Array5, Array6);
        Span<byte> expected = Concat(Array1, Array2, Array3, Array4, Array5, Array6);
        Assert.IsTrue(concatenated.SequenceEqual(expected));
    }
    
    [TestMethod]
    public void Concat_TwoSpansBothEmpty()
    {
        Span<byte> empty1 = Span<byte>.Empty;
        Span<byte> empty2 = Span<byte>.Empty;
        Span<byte> concatenated = stackalloc byte[empty1.Length + empty2.Length];
        Spans.Concat(concatenated, empty1, empty2);
        Span<byte> expected = Concat(empty1.ToArray(), empty2.ToArray());
        Assert.IsTrue(concatenated.SequenceEqual(expected));
    }
    
    [TestMethod]
    public void Concat_TwoSpansOneEmpty()
    {
        Span<byte> empty = Span<byte>.Empty;
        Span<byte> concatenated = stackalloc byte[empty.Length + Array2.Length];
        Spans.Concat(concatenated, empty, Array2);
        Span<byte> expected = Concat(empty.ToArray(), Array2);
        Assert.IsTrue(concatenated.SequenceEqual(expected));
        Spans.Concat(concatenated, Array2, empty);
        Assert.IsTrue(concatenated.SequenceEqual(expected));
    }
}