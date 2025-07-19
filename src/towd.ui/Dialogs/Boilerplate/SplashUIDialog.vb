Imports towd.business

Public Class SplashUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)

    Public Sub New(context As IUIContext(Of IWorld))
        Me.context = context
        Debug.Assert(Me.context IsNot Nothing)
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))({
                (Mood.Normal, ".___________.  ______   ____    __    ____  _______  ", True),
                (Mood.Normal, "|           | /  __  \  \   \  /  \  /   / |       \ ", True),
                (Mood.Normal, "`---|  |----`|  |  |  |  \   \/    \/   /  |  .--.  |", True),
                (Mood.Normal, "    |  |     |  |  |  |   \            /   |  |  |  |", True),
                (Mood.Normal, "    |  |     |  `--'  |    \    /\    /    |  '--'  |", True),
                (Mood.Normal, "    |__|      \______/      \__/  \__/     |_______/ ", True)
            })
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(Of IEnumerable(Of String))({"OK"})
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult(String.Empty)
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Return New MainMenuUIDialog(context)
    End Function
End Class
