Module Program
    Sub Main(args As String())
        Application.Init()
        Using mainView As New MainView
            Application.Run(mainView)
        End Using
        Application.Shutdown()
    End Sub
End Module
