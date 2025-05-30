﻿//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2024, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//_____________________________________________________________________________________________________________________________________

using TP.ConcurrentProgramming.Data;

namespace TP.ConcurrentProgramming.BusinessLogic
{
    public abstract class BusinessLogicAbstractAPI : IDisposable
    {
        #region Layer Factory

        public static BusinessLogicAbstractAPI GetBusinessLogicLayer()
        {
            return modelInstance.Value;
        }

        #endregion Layer Factory

        #region Layer API

        public static readonly Dimensions Dimensions = new DefaultDimensions(10.0, 100.0);

        public abstract void Start(int numberOfBalls, Action<IPosition, IBall> upperLayerBallAdditionHandler, Action<IBall> upperLayerBallRemovalHandler);
        public abstract void Stop();
        public abstract void AddBall();
        public abstract void RemoveBall();
        public abstract void ApplyCollision(BallMovement movement);

        #endregion Layer API

        #region IDisposable

        public abstract void Dispose();

        #endregion IDisposable

        #region private

        private static Lazy<BusinessLogicAbstractAPI> modelInstance = new Lazy<BusinessLogicAbstractAPI>(() => new BusinessLogicImplementation());

        public static Dimensions GetDimensions { get; internal set; }

        #endregion private
    }
    /// <summary>
    /// Immutable type representing table dimensions
    /// </summary>
    /// <param name="BallDimension"></param>
    /// <param name="TableSize"></param>
    /// <remarks>
    /// Must be abstract
    /// </remarks>
    public abstract class Dimensions
    {
        public abstract double BallDimension { get; }
        public abstract double TableSize { get; }
        public double TableHeight { get; internal set; }
        public double TableWidth { get; internal set; }
    }

    internal class DefaultDimensions : Dimensions
    {
        public DefaultDimensions(double ballDiameter, double tableSize)
        {
            BallDimension = ballDiameter;
            TableSize = tableSize;
        }

        public override double BallDimension { get; }
        public override double TableSize { get; }
    }

    public interface IPosition
    {
        double x { get; init; }
        double y { get; init; }
    }

    public interface IBall
    {
        event EventHandler<IPosition> NewPositionNotification;
    }

    public record BallMovement(IBall ball, IVector delta);
}