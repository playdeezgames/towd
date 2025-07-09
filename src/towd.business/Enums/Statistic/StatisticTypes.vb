Imports System.Runtime.CompilerServices
Imports towd.data

Public Module StatisticTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of String, IStatisticType) =
        New List(Of IStatisticType) From
        {
            New StatisticTypeDescriptor(StatisticType.BuildCounter, "Builds", "Counts how many times you build stuff!"),
            New StatisticTypeDescriptor(StatisticType.DigSkill, "Dig Skill", "Level of skill at digging. Cuz digging is a skill."),
            New StatisticTypeDescriptor(StatisticType.Digging, "Digging", "Counts number of times you have dug."),
            New StatisticTypeDescriptor(StatisticType.ChopSkill, "Chop Skill", "Level of skill at chopping. Wood in this case, because we don't have onions."),
            New StatisticTypeDescriptor(StatisticType.Chopping, "Chopping", "Counts the number of times you have chopped. Chop chop. Chip chop chip."),
            New StatisticTypeDescriptor(StatisticType.CookingCounter, "Cooking", "Counts the number of times you have cooked. Not meth, but other things."),
            New StatisticTypeDescriptor(StatisticType.CraftCounter, "Crafting", "Counts the number of times you have crafted."),
            New StatisticTypeDescriptor(StatisticType.Durability, "Durability", "This represents the number of times something can be used before it is worn out."),
            New StatisticTypeDescriptor(StatisticType.Fishing, "Fishing", "This counts the number of times you have fished."),
            New StatisticTypeDescriptor(StatisticType.FishSkill, "Fish Skill", "Level of skill at fishing. Why is there always fishing in these games?"),
            New StatisticTypeDescriptor(StatisticType.ForagingCounter, "Foraging", "This counts the number of times you have foraged."),
            New StatisticTypeDescriptor(StatisticType.ForagingSkill, "Foraging Skill", "Level of skill at foraging. Because nothing says ""fun"" like grabbing plant fiber!"),
            New StatisticTypeDescriptor(StatisticType.Fuel, "Fuel", "Amount of burnable material that produces heat. Has nothing to do with a snabel."),
            New StatisticTypeDescriptor(StatisticType.Health, "Health", "If you have more than zero, you are not dead!"),
            New StatisticTypeDescriptor(StatisticType.KnappingSkill, "Knapping Skill", "Yer skill level with knapping. Has nothing to do with sleep."),
            New StatisticTypeDescriptor(StatisticType.Satiety, "Satiety", "If games didn't have hunger mechanics, I would not know the word ""satiety"", but because they do, I can tell you that this mean the OPPOSITE of hunger."),
            New StatisticTypeDescriptor(StatisticType.SmeltingCounter, "Smelting", "Number of times you have taken ore and purified it."),
            New StatisticTypeDescriptor(StatisticType.Steps, "Steps", "Every step you take. Every move you make. Every vow you break. I don't know the rest of the words, but I'll be watching you."),
            New StatisticTypeDescriptor(StatisticType.XP, "XP", "Because, like in life, we do random things and collect experience. And the we take that experience and spend it on completely new skills we have never used before, and we are suddenly good at them. Right?")
        }.ToDictionary(Function(x) x.StatisticType, Function(x) x)
    <Extension>
    Public Function ToStatisticTypeDescriptor(statisticType As String) As IStatisticType
        Return Descriptors(statisticType)
    End Function
End Module
