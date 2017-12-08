using System;
using System.Collections;
using System.Collections.Generic;

namespace TagCloud.Core.Math
{
    public class Circle : IEnumerable<Vector>
    {
        private readonly double _radius;
        private readonly double _segments;

        public Circle(double radius, int segments)
        {
            _radius = radius;
            _segments = segments;
        }

        public IEnumerator<Vector> GetEnumerator()
        {
            return new CircleEnumerator(_radius, _segments);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class CircleEnumerator : IEnumerator<Vector>
        {
            private readonly double _radius;
            private readonly double _segments;
            private readonly double _dTheta;

            private bool _isDisposed;
            private Vector _current;
            private int _currentSector;

            public Vector Current
            {
                get
                {
                    ThrowIfDisposed();
                    return _current;
                }
                private set => _current = value;
            }

            object IEnumerator.Current => Current;

            public CircleEnumerator(double radius, double segments)
            {
                _radius = radius;
                _segments = segments;

                _dTheta = 2 * System.Math.PI / segments;
            }

            public void Dispose()
            {
                ThrowIfDisposed();
                _isDisposed = true;
            }

            public bool MoveNext()
            {
                ThrowIfDisposed();

                if (_currentSector >= _segments)
                {
                    return false;
                }

                double theta = _currentSector * _dTheta;

                Current = new Vector(_radius * System.Math.Cos(theta), _radius * System.Math.Sin(theta));

                _currentSector++;
                return true;
            }

            public void Reset()
            {
                _currentSector = 0;
            }

            private void ThrowIfDisposed()
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("CircleEnumerator");
                }
            }
        }
    }
}
