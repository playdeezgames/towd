Imports System.Runtime.CompilerServices
Imports towd.data

Public Module CharacterTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of CharacterType, ICharacterType) =
        New List(Of ICharacterType) From
        {
            New N00bCharacterTypeDescriptor(),
            New THINDLACharacterTypeDescriptor(),
            New THINDLAssCharacterTypeDescriptor()
        }.
        ToDictionary(Function(x) x.CharacterType, Function(x) x)
    <Extension>
    Public Function ToDescriptor(characterType As CharacterType) As ICharacterType
        Return Descriptors(characterType)
    End Function
End Module
