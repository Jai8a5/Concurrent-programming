﻿//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2024, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//_____________________________________________________________________________________________________________________________________

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

    public static readonly Dimensions GetDimensions = new DefaultDimensions(10.0, 10.0, 10.0);

    public abstract void Start(int numberOfBalls, Action<IPosition, IBall> upperLayerBallAdditionHandler, Action<IBall> upperLayerBallRemovalHandler);
    public abstract void Stop();
    public abstract void AddBall();
    public abstract void RemoveBall();

    #region IDisposable

    public abstract void Dispose();

    #endregion IDisposable

    #endregion Layer API

    #region private

    private static Lazy<BusinessLogicAbstractAPI> modelInstance = new Lazy<BusinessLogicAbstractAPI>(() => new BusinessLogicImplementation());

    #endregion private
  }
  /// <summary>
  /// Immutable type representing table dimensions
  /// </summary>
  /// <param name="BallDimension"></param>
  /// <param name="TableHeight"></param>
  /// <param name="TableWidth"></param>
  /// <remarks>
  /// Must be abstract
  /// </remarks>
  public abstract class Dimensions
  {
    public abstract double BallDimension { get; }
    public abstract double TableHeight { get; }
    public abstract double TableWidth { get; }
  }

  internal class DefaultDimensions : Dimensions
  {
    public DefaultDimensions(double ball, double height, double width)
    {
        BallDimension = ball;
        TableHeight = height;
        TableWidth = width;
    }

    public override double BallDimension { get; }
    public override double TableHeight { get; }
    public override double TableWidth { get; }
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
}