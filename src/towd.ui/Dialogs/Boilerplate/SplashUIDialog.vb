Imports towd.business

Public Class SplashUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)

    Public Sub New(context As IUIContext(Of IWorld))
        Me.context = context
        Debug.Assert(Me.context IsNot Nothing)
    End Sub

    Public Function GetLines() As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.GetLines
        Return {
                (Mood.Normal, ".___________.  ______   ____    __    ____  _______  ", True),
                (Mood.Normal, "|           | /  __  \  \   \  /  \  /   / |       \ ", True),
                (Mood.Normal, "`---|  |----`|  |  |  |  \   \/    \/   /  |  .--.  |", True),
                (Mood.Normal, "    |  |     |  |  |  |   \            /   |  |  |  |", True),
                (Mood.Normal, "    |  |     |  `--'  |    \    /\    /    |  '--'  |", True),
                (Mood.Normal, "    |__|      \______/      \__/  \__/     |_______/ ", True)
            }
    End Function

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

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Return New MainMenuUIDialog(context)
    End Function
End Class
