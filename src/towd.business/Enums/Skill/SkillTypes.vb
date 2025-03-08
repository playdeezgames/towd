Imports System.Runtime.CompilerServices
Imports towd.data

Public Module SkillTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of SkillType, ISkillType) =
        New List(Of ISkillType) From
        {
            New ForagingSkillTypeDescriptor()
        }.ToDictionary(Function(x) x.SkillType, Function(x) x)
    <Extension>
    Friend Function ToDescriptor(skillType As SkillType) As ISkillType
        Return Descriptors(skillType)
    End Function
End Module
