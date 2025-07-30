Imports towd.business

Friend Class VerbMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Const ALL_VERBS_TEXT = "All Verbs..."
    Private table As Dictionary(Of String, Func(Of IUIDialog)) = Nothing

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))(Array.Empty(Of UIDialogLine))
    End Function

    Public Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Return Task.FromResult(Of IReadOnlyDictionary(Of String, String))(Nothing)
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT,
                    ALL_VERBS_TEXT
                }
        If table Is Nothing Then
            table = VerbTypes.Descriptors.Values.
            Where(AddressOf VerbTypeFilter).
            GroupBy(Function(x) x.VerbCategoryType).
            ToDictionary(
                AddressOf getTableKey,
                AddressOf getTableValue)
        End If
        result.AddRange(table.Keys)
        Return Task.FromResult(Of IEnumerable(Of String))(result)
    End Function

    Private Function getTableValue(grouping As IGrouping(Of String, IVerbType)) As Func(Of IUIDialog)
        If grouping.Count = 1 Then
            Return Function() As IUIDialog
                       Return New VerbDetailUIDialog(context, grouping.Single, False, MakeCopy)
                   End Function
        End If
        Return Function() As IUIDialog
                   Return New FilteredVerbCategoryUIDialog(context, grouping.Key.ToVerbCategoryDescriptor.Name, grouping.Key, AddressOf ActualVerbTypeFilter, MakeCopy)
               End Function
    End Function

    Private Function ActualVerbTypeFilter(type As IVerbType, character As ICharacter) As Boolean
        Return type.CanPerform(character)
    End Function

    Private Function VerbTypeFilter(type As IVerbType) As Boolean
        Return ActualVerbTypeFilter(type, context.World.Avatar)
    End Function

    Private Function getTableKey(grouping As IGrouping(Of String, IVerbType)) As String
        If grouping.Count = 1 Then
            Return grouping.Single.Name
        End If
        Return $"{grouping.Key.ToVerbCategoryDescriptor.Name}..."
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Verbs")
    End Function

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case ALL_VERBS_TEXT
                Return Task.FromResult(Of IUIDialog)(New FilteredVerbUIDialog(context, "All Verbs", Function(verbType As IVerbType, character As ICharacter) True, MakeCopy))
            Case NEVER_MIND_TEXT
                Return Task.FromResult(cancelDialog())
            Case Else
                Dim nextDialog As Func(Of IUIDialog) = Nothing
                If table.TryGetValue(choice, nextDialog) Then
                    Return Task.FromResult(nextDialog())
                End If
                Throw New NotImplementedException
        End Select
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New VerbMenuUIDialog(context, cancelDialog))
    End Function
End Class
