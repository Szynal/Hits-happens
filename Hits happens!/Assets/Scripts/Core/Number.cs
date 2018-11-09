using System;

namespace Assets.Scripts.Core
{
    /// <summary>
    /// Representation of Number class (: ICloneable) for various data types such as int, float, double.
    /// <para> Number ValueType { Constant, RandomBetweenTwoConstants, RandomValueFromRange, ProportionalToDestructionLevel} </para>  
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Number<T> : ICloneable
    {
        public enum ValueType { Constant, RandomBetweenTwoConstants, RandomValueFromRange, ProportionalToDestructionLevel }

        public readonly T Value;
        public T[] MultipleValues;

        public ValueType Type;

        public Number() { }

        public Number(T value, ValueType valueType)
        {
            Value = value;

            switch (valueType)
            {
                case ValueType.RandomBetweenTwoConstants:
                case ValueType.RandomValueFromRange:
                    MultipleValues = new T[] { value, Value };
                    break;
                case ValueType.Constant:
                    break;
                case ValueType.ProportionalToDestructionLevel:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("valueType", valueType, null);
            }
        }

        public Number(T value, T[] multipleValues, ValueType type)
        {
            Value = value;
            MultipleValues = multipleValues;
            Type = type;
        }

        public override bool Equals(object obj)
        {
            return obj != null && (obj is T ? this.Value.Equals((T)obj) : obj is Number<T> && this.Value.Equals(((Number<T>)obj).Value));
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public T GetValue()
        {
            var returnValue = Value;

            switch (Type)
            {
                case ValueType.RandomBetweenTwoConstants:
                    returnValue = MultipleValues[UnityEngine.Random.Range(0, 2)];
                    break;
                case ValueType.RandomValueFromRange:
                    if (typeof(T) == typeof(int))
                    {
                        returnValue = (T)Convert.ChangeType(UnityEngine.Random.Range(Convert.ToInt32(MultipleValues[0]), Convert.ToInt32(MultipleValues[1])), typeof(T));
                    }

                    if (typeof(T) == typeof(float) || typeof(T) == typeof(double))
                    {
                        returnValue = (T)Convert.ChangeType(UnityEngine.Random.Range(Convert.ToSingle(MultipleValues[0]), Convert.ToSingle(MultipleValues[1])), typeof(T));
                    }

                    break;
                case ValueType.Constant:
                    break;
                case ValueType.ProportionalToDestructionLevel:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return returnValue;
        }

        public object Clone()
        {
            var number = (Number<T>)MemberwiseClone();
            return number;
        }
    }

    public class NumberInt : Number<int> { }

    public class NumberFloat : Number<float> { }

    public class NumberDouble : Number<double> { }
}
