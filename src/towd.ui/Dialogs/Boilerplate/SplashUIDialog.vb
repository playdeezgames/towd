Imports towd.business

Public Class SplashUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)

    Public Sub New(context As IUIContext(Of IWorld))
        Me.context = context
        Debug.Assert(Me.context IsNot Nothing)
    End Sub

    Public Function GetLines() As IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)) Implements IUIDialog.GetLines
        Return {
                (Mood.Normal, ".___________.  ______   ____    __    ____  _______  ", True),
                (Mood.Normal, "|           | /  __  \  \   \  /  \  /   / |       \ ", True),
                (Mood.Normal, "`---|  |----`|  |  |  |  \   \/    \/   /  |  .--.  |", True),
                (Mood.Normal, "    |  |     |  |  |  |   \            /   |  |  |  |", True),
                (Mood.Normal, "    |  |     |  `--'  |    \    /\    /    |  '--'  |", True),
                (Mood.Normal, "    |__|      \______/      \__/  \__/     |_______/ ", True)
            }
    End Function

    Public Function GetChoices() As IEnumerable(Of String) Implements IUIDialog.GetChoices
        Return {"OK"}
    End Function

    Public Function GetPrompt() As String Implements IUIDialog.GetPrompt
        Return String.Empty
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Return New MainMenuUIDialog(context)
    End Function
End Class
