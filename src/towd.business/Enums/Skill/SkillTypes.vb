Imports System.Runtime.CompilerServices
Imports towd.data

Public Module SkillTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of String, ISkillType) =
        New List(Of ISkillType) From
        {
            New ForagingSkillTypeDescriptor(),
            New KnappingSkillTypeDescriptor(),
            New DigSkillTypeDescriptor(),
            New ChopSkillTypeDescriptor(),
            New FishSkillTypeDescriptor()
        }.ToDictionary(Function(x) x.SkillType, Function(x) x)
    <Extension>
    Public Function ToSkillTypeDescriptor(skillType As String) As ISkillType
        Return Descriptors(skillType)
    End Function
End Module
