﻿using static Interop.Libsodium;

namespace Geralt;

public static class ConstantTime
{
    public static unsafe bool Equals(ReadOnlySpan<byte> a, ReadOnlySpan<byte> b)
    {
        Validation.NotEmpty(nameof(a), a.Length);
        Validation.NotEmpty(nameof(b), b.Length);
        Sodium.Initialise();
        // It's impossible to prevent the lengths being leaked
        if (a.Length != b.Length) { return false; }
        fixed (byte* aPtr = a, bPtr = b)
            return sodium_memcmp(aPtr, bPtr, (nuint)a.Length) == 0;
    }

    public static unsafe void Increment(Span<byte> buffer)
    {
        Validation.NotEmpty(nameof(buffer), buffer.Length);
        Sodium.Initialise();
        fixed (byte* b = buffer)
            sodium_increment(b, (nuint)buffer.Length);
    }

    public static unsafe void Add(Span<byte> buffer, ReadOnlySpan<byte> a, ReadOnlySpan<byte> b)
    {
        Validation.NotEmpty(nameof(buffer), buffer.Length);
        Validation.NotEmpty(nameof(a), a.Length);
        Validation.NotEmpty(nameof(b), b.Length);
        Validation.EqualToSize(nameof(a), a.Length, buffer.Length);
        Validation.EqualToSize(nameof(a), a.Length, b.Length);
        Sodium.Initialise();
        a.CopyTo(buffer);
        fixed (byte* aPtr = buffer, bPtr = b)
            sodium_add(aPtr, bPtr, (nuint)buffer.Length);
    }

    public static unsafe void Subtract(Span<byte> buffer, ReadOnlySpan<byte> a, ReadOnlySpan<byte> b)
    {
        Validation.NotEmpty(nameof(buffer), buffer.Length);
        Validation.NotEmpty(nameof(a), a.Length);
        Validation.NotEmpty(nameof(b), b.Length);
        Validation.EqualToSize(nameof(a), a.Length, buffer.Length);
        Validation.EqualToSize(nameof(a), a.Length, b.Length);
        Sodium.Initialise();
        a.CopyTo(buffer);
        fixed (byte* aPtr = buffer, bPtr = b)
            sodium_sub(aPtr, bPtr, (nuint)buffer.Length);
    }

    public static unsafe bool IsLessThan(ReadOnlySpan<byte> a, ReadOnlySpan<byte> b)
    {
        Validation.NotEmpty(nameof(a), a.Length);
        Validation.NotEmpty(nameof(b), b.Length);
        Validation.EqualToSize(nameof(a), a.Length, b.Length);
        Sodium.Initialise();
        fixed (byte* aPtr = a, bPtr = b)
            return sodium_compare(aPtr, bPtr, (nuint)a.Length) == -1;
    }

    public static unsafe bool IsGreaterThan(ReadOnlySpan<byte> a, ReadOnlySpan<byte> b)
    {
        Validation.NotEmpty(nameof(a), a.Length);
        Validation.NotEmpty(nameof(b), b.Length);
        Validation.EqualToSize(nameof(a), a.Length, b.Length);
        Sodium.Initialise();
        fixed (byte* aPtr = a, bPtr = b)
            return sodium_compare(aPtr, bPtr, (nuint)a.Length) == 1;
    }

    public static unsafe bool IsAllZeros(ReadOnlySpan<byte> buffer)
    {
        Sodium.Initialise();
        fixed (byte* b = buffer)
            return sodium_is_zero(b, (nuint)buffer.Length) == 1;
    }
}