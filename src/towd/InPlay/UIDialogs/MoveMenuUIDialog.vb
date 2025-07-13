Imports NStack
Imports towd.business

Friend Class MoveMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Private ReadOnly cancelDialog As IUIDialog
    Private table As New Dictionary(Of String, IDirection)
    Const NEVER_MIND_TEXT = "Never Mind"

    Public Sub New(context As IUIContext, cancelDialog As IUIDialog)
        Me.context = context
        Me.cancelDialog = cancelDialog
        table.Add(NEVER_MIND_TEXT, Nothing)
        For Each descriptor In context.World.Avatar.CurrentLocation.Neighbors.Select(Function(x) x.Key.ToDirectionDescriptor)
            table.Add(descriptor.Name, descriptor)
        Next
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Return {"Choose a direction."}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return table.Keys
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Which Way?"
        End Get
    End Property

    Public Function Choose(choice As String) As (String, IUIDialog) Implements IUIDialog.Choose
        Dim descriptor = table(choice)
        If descriptor Is Nothing Then
            Return (Nothing, cancelDialog)
        End If
        context.World.Avatar.Move(descriptor.Direction)
        context.World.AdvanceTime(1)
        Return NeutralUIDialog.DetermineInPlayDialog(context)
    End Function
End Class
