Imports System.Runtime.CompilerServices
Imports towd.data

Friend Module CharacterTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of CharacterType, ICharacterType) =
        New List(Of ICharacterType) From
        {
            New N00bCharacterTypeDescriptor()
        }.
        ToDictionary(Function(x) x.CharacterType, Function(x) x)
    <Extension>
    Friend Function ToDescriptor(characterType As CharacterType) As ICharacterType
        Return Descriptors(characterType)
    End Function
End Module
