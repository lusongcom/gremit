using System;
using System.Collections.Generic;
using System.Reflection.Emit;

using GrEmit.StackMutators;

namespace GrEmit
{
    internal static class StackMutatorCollection
    {
        public static void Mutate(OpCode opCode, GroboIL il, ILInstructionParameter parameter, ref Stack<Type> stack)
        {
            StackMutator stackMutator;
            if(opCode == default(OpCode))
                stackMutator = markLabelStackMutator;
            else if(!stackMutators.TryGetValue(opCode, out stackMutator))
                throw new NotSupportedException("OpCode '" + opCode + "' is not supported");
            stackMutator.Mutate(il, parameter, ref stack);
        }

        private static readonly Dictionary<OpCode, StackMutator> stackMutators = new Dictionary<OpCode, StackMutator>
            {
                {OpCodes.Ret, new RetStackMutator()},
                {OpCodes.Br, new BrStackMutator()},
                {OpCodes.Br_S, new BrStackMutator()},
                {OpCodes.Brfalse, new BrfalseStackMutator()},
                {OpCodes.Brfalse_S, new BrfalseStackMutator()},
                {OpCodes.Brtrue, new BrfalseStackMutator()},
                {OpCodes.Brtrue_S, new BrfalseStackMutator()},
                {OpCodes.Beq, new BeqStackMutator()},
                {OpCodes.Beq_S, new BeqStackMutator()},
                {OpCodes.Bne_Un, new BeqStackMutator()},
                {OpCodes.Bne_Un_S, new BeqStackMutator()},
                {OpCodes.Ble, new BleStackMutator()},
                {OpCodes.Ble_S, new BleStackMutator()},
                {OpCodes.Ble_Un, new BleStackMutator()},
                {OpCodes.Ble_Un_S, new BleStackMutator()},
                {OpCodes.Blt, new BleStackMutator()},
                {OpCodes.Blt_S, new BleStackMutator()},
                {OpCodes.Blt_Un, new BleStackMutator()},
                {OpCodes.Blt_Un_S, new BleStackMutator()},
                {OpCodes.Bge, new BleStackMutator()},
                {OpCodes.Bge_S, new BleStackMutator()},
                {OpCodes.Bge_Un, new BleStackMutator()},
                {OpCodes.Bge_Un_S, new BleStackMutator()},
                {OpCodes.Bgt, new BleStackMutator()},
                {OpCodes.Bgt_S, new BleStackMutator()},
                {OpCodes.Bgt_Un, new BleStackMutator()},
                {OpCodes.Bgt_Un_S, new BleStackMutator()},
                {OpCodes.Pop, new PopStackMutator()},
                {OpCodes.Dup, new DupStackMutator()},
                {OpCodes.Ldloc, new LdlocStackMutator()},
                {OpCodes.Ldloc_0, new LdlocStackMutator()},
                {OpCodes.Ldloc_1, new LdlocStackMutator()},
                {OpCodes.Ldloc_2, new LdlocStackMutator()},
                {OpCodes.Ldloc_3, new LdlocStackMutator()},
                {OpCodes.Ldloc_S, new LdlocStackMutator()},
                {OpCodes.Ldloca, new LdlocaStackMutator()},
                {OpCodes.Ldloca_S, new LdlocaStackMutator()},
                {OpCodes.Stloc, new StlocStackMutator()},
                {OpCodes.Stloc_0, new StlocStackMutator()},
                {OpCodes.Stloc_1, new StlocStackMutator()},
                {OpCodes.Stloc_2, new StlocStackMutator()},
                {OpCodes.Stloc_3, new StlocStackMutator()},
                {OpCodes.Stloc_S, new StlocStackMutator()},
                {OpCodes.Ldnull, new LdnullStackMutator()},
                {OpCodes.Ldarg_0, new Ldarg_0StackMutator()},
                {OpCodes.Ldarg_1, new Ldarg_1StackMutator()},
                {OpCodes.Ldarg_2, new Ldarg_2StackMutator()},
                {OpCodes.Ldarg_3, new Ldarg_3StackMutator()},
                {OpCodes.Ldarg_S, new Ldarg_SStackMutator()},
                {OpCodes.Ldarg, new LdargStackMutator()},
                {OpCodes.Ldarga_S, new Ldarga_SStackMutator()},
                {OpCodes.Ldarga, new LdargaStackMutator()},
                {OpCodes.Ldc_I4, new Ldc_I4StackMutator()},
                {OpCodes.Ldc_I4_0, new Ldc_I4StackMutator()},
                {OpCodes.Ldc_I4_1, new Ldc_I4StackMutator()},
                {OpCodes.Ldc_I4_2, new Ldc_I4StackMutator()},
                {OpCodes.Ldc_I4_3, new Ldc_I4StackMutator()},
                {OpCodes.Ldc_I4_4, new Ldc_I4StackMutator()},
                {OpCodes.Ldc_I4_5, new Ldc_I4StackMutator()},
                {OpCodes.Ldc_I4_6, new Ldc_I4StackMutator()},
                {OpCodes.Ldc_I4_7, new Ldc_I4StackMutator()},
                {OpCodes.Ldc_I4_8, new Ldc_I4StackMutator()},
                {OpCodes.Ldc_I4_M1, new Ldc_I4StackMutator()},
                {OpCodes.Ldc_I4_S, new Ldc_I4StackMutator()},
                {OpCodes.Ldc_I8, new Ldc_I8StackMutator()},
                {OpCodes.Ldc_R4, new Ldc_R4StackMutator()},
                {OpCodes.Ldc_R8, new Ldc_R8StackMutator()},
                {OpCodes.Ldstr, new LdstrStackMutator()},
                {OpCodes.Ldlen, new LdlenStackMutator()},
                {OpCodes.Call, new CallStackMutator()},
                {OpCodes.Callvirt, new CallStackMutator()},
                {OpCodes.Calli, new CalliStackMutator()},
                {OpCodes.Ldftn, new LdftnStackMutator()},
                {OpCodes.Ldfld, new LdfldStackMutator()},
                {OpCodes.Ldflda, new LdfldaStackMutator()},
                {OpCodes.Stfld, new StfldStackMutator()},
                {OpCodes.Ldelema, new LdelemaStackMutator()},
                {OpCodes.Ldelem, new LdelemStackMutator()},
                {OpCodes.Ldelem_I1, new LdelemStackMutator()},
                {OpCodes.Ldelem_I2, new LdelemStackMutator()},
                {OpCodes.Ldelem_I4, new LdelemStackMutator()},
                {OpCodes.Ldelem_I8, new LdelemStackMutator()},
                {OpCodes.Ldelem_R4, new LdelemStackMutator()},
                {OpCodes.Ldelem_R8, new LdelemStackMutator()},
                {OpCodes.Ldelem_Ref, new LdelemStackMutator()},
                {OpCodes.Ldelem_U1, new LdelemStackMutator()},
                {OpCodes.Ldelem_U2, new LdelemStackMutator()},
                {OpCodes.Ldelem_U4, new LdelemStackMutator()},
                {OpCodes.Stelem, new StelemStackMutator()},
                {OpCodes.Stelem_I1, new StelemStackMutator()},
                {OpCodes.Stelem_I2, new StelemStackMutator()},
                {OpCodes.Stelem_I4, new StelemStackMutator()},
                {OpCodes.Stelem_I8, new StelemStackMutator()},
                {OpCodes.Stelem_R4, new StelemStackMutator()},
                {OpCodes.Stelem_R8, new StelemStackMutator()},
                {OpCodes.Stelem_Ref, new StelemStackMutator()},
                {OpCodes.Stind_I1, new StindStackMutator()},
                {OpCodes.Stind_I2, new StindStackMutator()},
                {OpCodes.Stind_I4, new StindStackMutator()},
                {OpCodes.Stind_I8, new StindStackMutator()},
                {OpCodes.Stind_R4, new StindStackMutator()},
                {OpCodes.Stind_R8, new StindStackMutator()},
                {OpCodes.Stind_Ref, new StindStackMutator()},
                {OpCodes.Stobj, new StindStackMutator()},
                {OpCodes.Ldind_I1, new LdindStackMutator()},
                {OpCodes.Ldind_I2, new LdindStackMutator()},
                {OpCodes.Ldind_I4, new LdindStackMutator()},
                {OpCodes.Ldind_I8, new LdindStackMutator()},
                {OpCodes.Ldind_R4, new LdindStackMutator()},
                {OpCodes.Ldind_R8, new LdindStackMutator()},
                {OpCodes.Ldind_Ref, new LdindStackMutator()},
                {OpCodes.Ldind_U1, new LdindStackMutator()},
                {OpCodes.Ldind_U2, new LdindStackMutator()},
                {OpCodes.Ldind_U4, new LdindStackMutator()},
                {OpCodes.Ldobj, new LdindStackMutator()},
                {OpCodes.Castclass, new CastclassStackMutator()},
                {OpCodes.Unbox_Any, new Unbox_AnyStackMutator()},
                {OpCodes.Box, new BoxStackMutator()},
                {OpCodes.Constrained, new ConstrainedStackMutator()},
                {OpCodes.Newobj, new NewobjStackMutator()},
                {OpCodes.Initobj, new InitobjStackMutator()},
                {OpCodes.Newarr, new NewarrStackMutator()},
                {OpCodes.Ceq, new CeqStackMutator()},
                {OpCodes.Cgt, new CgtStackMutator()},
                {OpCodes.Cgt_Un, new CgtStackMutator()},
                {OpCodes.Clt, new CgtStackMutator()},
                {OpCodes.Clt_Un, new CgtStackMutator()},
                {OpCodes.And, new MathOpStackMutator()},
                {OpCodes.Or, new MathOpStackMutator()},
                {OpCodes.Xor, new MathOpStackMutator()},
                {OpCodes.Add, new MathOpStackMutator()},
                {OpCodes.Add_Ovf, new MathOpStackMutator()},
                {OpCodes.Add_Ovf_Un, new MathOpStackMutator()},
                {OpCodes.Sub, new MathOpStackMutator()},
                {OpCodes.Sub_Ovf, new MathOpStackMutator()},
                {OpCodes.Sub_Ovf_Un, new MathOpStackMutator()},
                {OpCodes.Mul, new MathOpStackMutator()},
                {OpCodes.Mul_Ovf, new MathOpStackMutator()},
                {OpCodes.Mul_Ovf_Un, new MathOpStackMutator()},
                {OpCodes.Div, new MathOpStackMutator()},
                {OpCodes.Div_Un, new MathOpStackMutator()},
                {OpCodes.Rem, new MathOpStackMutator()},
                {OpCodes.Rem_Un, new MathOpStackMutator()},
                {OpCodes.Shl, new MathOpStackMutator()},
                {OpCodes.Shr, new MathOpStackMutator()},
                {OpCodes.Shr_Un, new MathOpStackMutator()},
                {OpCodes.Conv_I1, new ConvI4StackMutator()},
                {OpCodes.Conv_I2, new ConvI4StackMutator()},
                {OpCodes.Conv_I4, new ConvI4StackMutator()},
                {OpCodes.Conv_I8, new ConvI8StackMutator()},
                {OpCodes.Conv_U1, new ConvI4StackMutator()},
                {OpCodes.Conv_U2, new ConvI4StackMutator()},
                {OpCodes.Conv_U4, new ConvI4StackMutator()},
                {OpCodes.Conv_U8, new ConvI8StackMutator()},
                {OpCodes.Conv_R4, new ConvR4StackMutator()},
                {OpCodes.Conv_R8, new ConvR8StackMutator()},
                {OpCodes.Conv_R_Un, new ConvR4StackMutator()},
            };

        private static readonly MarkLabelStackMutator markLabelStackMutator = new MarkLabelStackMutator();
    }
}