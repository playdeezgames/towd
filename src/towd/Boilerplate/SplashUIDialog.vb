Friend Class SplashUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext

    Public Sub New(context As IUIContext)
        Me.context = context
        Debug.Assert(Me.context IsNot Nothing)
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Return {
                ".___________.  ______   ____    __    ____  _______  ",
                "|           | /  __  \  \   \  /  \  /   / |       \ ",
                "`---|  |----`|  |  |  |  \   \/    \/   /  |  .--.  |",
                "    |  |     |  |  |  |   \            /   |  |  |  |",
                "    |  |     |  `--'  |    \    /\    /    |  '--'  |",
                "    |__|      \______/      \__/  \__/     |_______/ "
            }
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return {"OK"}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return String.Empty
        End Get
    End Property

    Public Function Choose(choice As String) As (String, IUIDialog) Implements IUIDialog.Choose
        Return (GameState.MainMenu, Nothing)
    End Function
End Class
