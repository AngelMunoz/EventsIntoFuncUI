namespace WindowSample

module Counter =
  open Avalonia.Controls
  open Avalonia.FuncUI
  open Avalonia.FuncUI.DSL
  open Avalonia.Layout
  open Avalonia.FuncUI.Builder
  open System


  let view
    (
      initialHeight: float,
      initialWidth: float,
      windowSizeChanged: IObservable<float * float>
    ) =

    Component(fun ctx ->

      // set the initial size of the window
      let width = ctx.useState initialWidth
      let height = ctx.useState initialHeight
      // register the window size changed event with a use effect hook
      ctx.useEffect(
        (fun _ ->
          // subscribe to the window size changed event
          windowSizeChanged
          |> Observable.subscribe(fun (newWidth, newHeight) ->
            // update the state value with the new size
            width.Set(newWidth)
            height.Set(newHeight)
          )
        ),
        [ EffectTrigger.AfterInit ]
      )

      StackPanel.create [
        StackPanel.orientation Orientation.Horizontal
        StackPanel.verticalAlignment VerticalAlignment.Center
        StackPanel.horizontalAlignment HorizontalAlignment.Center
        StackPanel.spacing 12.
        StackPanel.children [
          TextBlock.create [ TextBlock.text "Window size: " ]
          TextBlock.create [ TextBlock.text $"%f{width.Current} x %f{height.Current}" ]
        ]
      ]
    )