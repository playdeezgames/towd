Imports System.Runtime.CompilerServices
Imports towd.data

Public Module CharacterTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of String, ICharacterType) =
        New List(Of ICharacterType) From
        {
            New N00bCharacterTypeDescriptor(),
            New THINDLACharacterTypeDescriptor(),
            New THINDLAsAssCharacterTypeDescriptor()
        }.
        ToDictionary(Function(x) x.CharacterType, Function(x) x)
    <Extension>
    Public Function ToCharacterTypeDescriptor(characterType As String) As ICharacterType
        Return Descriptors(characterType)
    End Function
End Module
