﻿//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2023, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//  by introducing yourself and telling us what you do with this community.
//_____________________________________________________________________________________________________________________________________

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TP.ConcurrentProgramming.BusinessLogic;
using static System.Formats.Asn1.AsnWriter;
using LogicIBall = TP.ConcurrentProgramming.BusinessLogic.IBall;

namespace TP.ConcurrentProgramming.Presentation.Model
{
    internal class ModelBall : IBall
    {
        public ModelBall(double top, double left, LogicIBall underneathBall)
        {
            TopBackingField = top;
            LeftBackingField = left;
            underneathBall.NewPositionNotification += NewPositionNotification;
        }

        #region IBall

        public double Top
        {
            get { return (TopBackingField - Radius) * ModelImplementation.Instance.Scale; }
            private set
            {
                if (TopBackingField == value)
                    return;
                TopBackingField = value;
                RaisePropertyChanged();
            }
        }

        public double Left
        {
            get { return (LeftBackingField - Radius) * ModelImplementation.Instance.Scale; }
            private set
            {
                if (LeftBackingField == value)
                    return;
                LeftBackingField = value;
                RaisePropertyChanged();
            }
        }

        public double Diameter
        {
            get { return DiameterBackingField * ModelImplementation.Instance.Scale; }
            init
            {
                if (DiameterBackingField != value)
                {
                    DiameterBackingField = value;
                    RaisePropertyChanged();
                }
            }
        }

        public void UpdateScale()
        {
            RaisePropertyChanged(nameof(Diameter));
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INotifyPropertyChanged

        #endregion IBall

        #region private

        private double TopBackingField;
        private double LeftBackingField;
        private double DiameterBackingField;

        private double Radius => DiameterBackingField / 2;

        private void NewPositionNotification(object sender, IPosition e)
        {
            Top = e.y; Left = e.x;
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion private

        #region testing instrumentation

        [Conditional("DEBUG")]
        internal void SetLeft(double x)
        { Left = x; }

        [Conditional("DEBUG")]
        internal void SetTop(double x)
        { Top = x; }

        #endregion testing instrumentation
    }
}