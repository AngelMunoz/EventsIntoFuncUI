namespace WindowSample

open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Input
open Avalonia.Themes.Fluent
open Avalonia.FuncUI.Hosts

type MainWindow() as this =
  inherit HostWindow()

  let windowSizeObs =
    this.SizeChanged
    |> Observable.map(fun event -> event.NewSize.Width, event.NewSize.Height)

  let width, height = 400.0, 400.0

  do
    base.Title <- "WindowSample"
    base.Width <- width
    base.Height <- height
    // to avoid coupling your code to the Window,
    // just pass an observable which is agnostic of the window
    this.Content <- Counter.view(height, width, windowSizeObs)

type App() =
  inherit Application()

  override this.Initialize() = this.Styles.Add(FluentTheme())

  override this.OnFrameworkInitializationCompleted() =
    match this.ApplicationLifetime with
    | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
      desktopLifetime.MainWindow <- MainWindow()
    | _ -> ()

module Program =

  [<EntryPoint>]
  let main (args: string[]) =
    AppBuilder
      .Configure<App>()
      .UsePlatformDetect()
      .StartWithClassicDesktopLifetime(args)