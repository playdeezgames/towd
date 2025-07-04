Imports System.Runtime.CompilerServices

Public Module VerbCategoryTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of VerbCategoryType, IVerbCategoryType) =
        New List(Of IVerbCategoryType) From
        {
            New AddFuelVerbCategoryTypeDescriptor(),
            New BuildVerbCategoryTypeDescriptor(),
            New ChopVerbCategoryTypeDescriptor(),
            New CookVerbCategoryTypeDescriptor(),
            New CraftVerbCategoryTypeDescriptor(),
            New DigVerbCategoryTypeDescriptor(),
            New EatVerbCategoryTypeDescriptor(),
            New FishVerbCategoryTypeDescriptor(),
            New ForageVerbCategoryTypeDescriptor(),
            New WaitVerbCategoryTypeDescriptor()
        }.
        ToDictionary(Function(x) x.VerbCategoryType, Function(x) x)
    <Extension>
    Public Function ToDescriptor(verbCategoryType As VerbCategoryType) As IVerbCategoryType
        Return Descriptors(verbCategoryType)
    End Function
End Module
