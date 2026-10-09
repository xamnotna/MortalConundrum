using UnityEngine;

public readonly struct EntityId : System.IEquatable<EntityId>
{
    public readonly int Value;

    public EntityId(int value) => Value = value;

    public bool Equals(EntityId other) => Value == other.Value;
    public override bool Equals(object obj) => obj is EntityId other && Equals(other);
    public override int GetHashCode() => Value;
}
