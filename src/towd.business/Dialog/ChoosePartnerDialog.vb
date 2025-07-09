Imports System.Reflection.Metadata.Ecma335

Friend Class ChoosePartnerDialog
    Implements IDialog
    Const NEVER_MIND_TEXT = "Never Mind"

    Private ReadOnly table As IReadOnlyDictionary(Of String, Func(Of IDialog))

    Public Sub New(character As ICharacter)
        Dim groups = character.Location.GetOtherCharacters(character).GroupBy(Function(x) x.Name)
        Dim dialogGenerators As New Dictionary(Of String, Func(Of IDialog))
        For Each group In groups
            If group.Count > 1 Then
                Dim otherCharacters = group.ToArray
                For Each index In Enumerable.Range(0, group.Count)
                    Dim otherCharacter = otherCharacters(index)
                    dialogGenerators.Add($"{group.Key}#{index + 1}", Function() character.StartDialog(otherCharacter))
                Next
            Else
                dialogGenerators.Add($"{group.Key}", Function() character.StartDialog(group.Single))
            End If
        Next
        dialogGenerators.Add(NEVER_MIND_TEXT, Function() Nothing)
        table = dialogGenerators
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return {"With whom or what would you like to dialog?"}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            Return table.Keys
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IDialog.Prompt
        Get
            Return "Dialog With..."
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
        Return table(choice)()
    End Function
End Class
