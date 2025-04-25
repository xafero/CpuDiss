namespace ObjDumper.Core
{
    /// <summary>
    /// Instructions of the Intel 8086 / 80186
    /// </summary>
    public enum X86
    {
        None = 0,

        /// <summary>
        /// ASCII adjust AL after addition
        /// </summary>
        AAA,

        /// <summary>
        /// ASCII adjust AX before division
        /// </summary>
        AAD,

        /// <summary>
        /// ASCII adjust AX after multiplication
        /// </summary>
        AAM,

        /// <summary>
        /// ASCII adjust AL after subtraction
        /// </summary>
        AAS,

        /// <summary>
        /// Add with carry
        /// </summary>
        ADC,

        /// <summary>
        /// Add
        /// </summary>
        ADD,

        /// <summary>
        /// Logical And
        /// </summary>
        AND,

        /// <summary>
        /// Check array index against bounds
        /// </summary>
        BOUND,

        /// <summary>
        /// Call procedure
        /// </summary>
        CALL,

        /// <summary>
        /// Convert byte to word
        /// </summary>
        CBW,

        /// <summary>
        /// Clear carry flag
        /// </summary>
        CLC,

        /// <summary>
        /// Clear direction flag
        /// </summary>
        CLD,

        /// <summary>
        /// Clear interrupt flag
        /// </summary>
        CLI,

        /// <summary>
        /// Complement carry flag
        /// </summary>
        CMC,

        /// <summary>
        /// Compare operands
        /// </summary>
        CMP,

        /// <summary>
        /// Compare bytes in memory
        /// </summary>
        CMPSB,

        /// <summary>
        /// Compare words
        /// </summary>
        CMPSW,

        /// <summary>
        /// Convert word to doubleword 	
        /// </summary>
        CWD,

        /// <summary>
        /// Decimal adjust AL after addition
        /// </summary>
        DAA,

        /// <summary>
        /// Decimal adjust AL after subtraction
        /// </summary>
        DAS,

        /// <summary>
        /// Decrement by 1
        /// </summary>
        DEC,

        /// <summary>
        /// Unsigned divide
        /// </summary>
        DIV,

        /// <summary>
        /// Enter stack frame
        /// </summary>
        ENTER,

        /// <summary>
        /// Used with floating-point unit
        /// </summary>
        ESC,

        /// <summary>
        /// Enter halt state
        /// </summary>
        HLT,

        /// <summary>
        /// Signed divide
        /// </summary>
        IDIV,

        /// <summary>
        /// Signed multiply in One-operand form
        /// </summary>
        IMUL,

        /// <summary>
        /// Input from port
        /// </summary>
        IN,

        /// <summary>
        /// Increment by 1
        /// </summary>
        INC,

        /// <summary>
        /// Input from port to string
        /// </summary>
        INSB,

        /// <summary>
        /// Input from port to string
        /// </summary>
        INSW,

        /// <summary>
        /// Call to interrupt
        /// </summary>
        INT,

        /// <summary>
        /// Call to interrupt if overflow
        /// </summary>
        INTO,

        /// <summary>
        /// Return from interrupt
        /// </summary>
        IRET,

        /// <summary>
        /// Jump if above (unsigned greater than)
        /// </summary>
        JA,

        /// <summary>
        /// Jump if above or equal (unsigned greater than or equal)
        /// </summary>
        JAE,

        /// <summary>
        /// Jump if below (unsigned less than)
        /// </summary>
        JB,

        /// <summary>
        /// Jump if below or equal (unsigned less than or equal)
        /// </summary>
        JBE,

        /// <summary>
        /// Jump if carry (CF=1)
        /// </summary>
        JC,

        /// <summary>
        /// Jump if CX is zero
        /// </summary>
        JCXZ,

        /// <summary>
        /// Jump if equal (ZF=1)
        /// </summary>
        JE,

        /// <summary>
        /// Jump if greater (signed greater than)
        /// </summary>
        JG,

        /// <summary>
        /// Jump if greater or equal (signed greater than or equal)
        /// </summary>
        JGE,

        /// <summary>
        /// Jump if less (signed less than)
        /// </summary>
        JL,

        /// <summary>
        /// Jump if less or equal (signed less than or equal)
        /// </summary>
        JLE,

        /// <summary>
        /// Jump
        /// </summary>
        JMP,

        /// <summary>
        /// Jump if not above (unsigned not greater than)
        /// </summary>
        JNA,

        /// <summary>
        /// Jump if not above or equal (unsigned not greater than or equal)
        /// </summary>
        JNAE,

        /// <summary>
        /// Jump if not below (unsigned not less than)
        /// </summary>
        JNB,

        /// <summary>
        /// Jump if not below or equal (unsigned not less than or equal)
        /// </summary>
        JNBE,

        /// <summary>
        /// Jump if not carry (CF=0)
        /// </summary>
        JNC,

        /// <summary>
        /// Jump if not equal (ZF=0)
        /// </summary>
        JNE,

        /// <summary>
        /// Jump if not greater (signed not greater than)
        /// </summary>
        JNG,

        /// <summary>
        /// Jump if not greater or equal (signed not greater than or equal)
        /// </summary>
        JNGE,

        /// <summary>
        /// Jump if not less (signed not less than)
        /// </summary>
        JNL,

        /// <summary>
        /// Jump if not less or equal (signed not less than or equal)
        /// </summary>
        JNLE,

        /// <summary>
        /// Jump if not overflow (OF=0)
        /// </summary>
        JNO,

        /// <summary>
        /// Jump if not parity (PF=0)
        /// </summary>
        JNP,

        /// <summary>
        /// Jump if not sign (SF=0)
        /// </summary>
        JNS,

        /// <summary>
        /// Jump if not zero (ZF=0)
        /// </summary>
        JNZ,

        /// <summary>
        /// Jump if overflow (OF=1)
        /// </summary>
        JO,

        /// <summary>
        /// Jump if parity (PF=1)
        /// </summary>
        JP,

        /// <summary>
        /// Jump if parity even (PF=1)
        /// </summary>
        JPE,

        /// <summary>
        /// Jump if parity odd (PF=0)
        /// </summary>
        JPO,

        /// <summary>
        /// Jump if sign (SF=1)
        /// </summary>
        JS,

        /// <summary>
        /// Jump if zero (ZF=1)
        /// </summary>
        JZ,

        /// <summary>
        /// Load FLAGS into AH register
        /// </summary>
        LAHF,

        /// <summary>
        /// Load DS:r with far pointer
        /// </summary>
        LDS,

        /// <summary>
        /// Load Effective Address
        /// </summary>
        LEA,

        /// <summary>
        /// Leave stack frame
        /// </summary>
        LEAVE,

        /// <summary>
        /// Load ES:r with far pointer
        /// </summary>
        LES,

        /// <summary>
        /// Assert BUS LOCK# signal
        /// </summary>
        LOCK,

        /// <summary>
        /// Load string byte
        /// </summary>
        LODSB,

        /// <summary>
        /// Load string word
        /// </summary>
        LODSW,

        /// <summary>
        /// Loop control
        /// </summary>
        LOOP,

        /// <summary>
        /// Loop while equal (ZF=1)
        /// </summary>
        LOOPE,

        /// <summary>
        /// Loop while not equal (ZF=0)
        /// </summary>
        LOOPNE,

        /// <summary>
        /// Loop while not zero (ZF=0)
        /// </summary>
        LOOPNZ,

        /// <summary>
        /// Loop while zero (ZF=1)
        /// </summary>
        LOOPZ,

        /// <summary>
        /// Move
        /// </summary>
        MOV,

        /// <summary>
        /// Move byte from string to string
        /// </summary>
        MOVSB,

        /// <summary>
        /// Move word from string to string
        /// </summary>
        MOVSW,

        /// <summary>
        /// Unsigned multiply
        /// </summary>
        MUL,

        /// <summary>
        /// Two's complement negation
        /// </summary>
        NEG,

        /// <summary>
        /// No operation
        /// </summary>
        NOP,

        /// <summary>
        /// Negate the operand / logical Not
        /// </summary>
        NOT,

        /// <summary>
        /// Logical Or
        /// </summary>
        OR,

        /// <summary>
        /// Output to port
        /// </summary>
        OUT,

        /// <summary>
        /// Output string to port
        /// </summary>
        OUTSB,

        /// <summary>
        /// Output string to port
        /// </summary>
        OUTSW,

        /// <summary>
        /// Pop data from stack
        /// </summary>
        POP,

        /// <summary>
        /// Pop all general purpose registers from stack
        /// </summary>
        POPA,

        /// <summary>
        /// Pop FLAGS register from stack
        /// </summary>
        POPF,

        /// <summary>
        /// Push data onto stack
        /// </summary>
        PUSH,

        /// <summary>
        /// Push all general purpose registers onto stack
        /// </summary>
        PUSHA,

        /// <summary>
        /// Push FLAGS onto stack
        /// </summary>
        PUSHF,

        /// <summary>
        /// Rotate left (with carry)
        /// </summary>
        RCL,

        /// <summary>
        /// Rotate right (with carry)
        /// </summary>
        RCR,

        /// <summary>
        /// Repeat string operation while CX not zero
        /// </summary>
        REP,

        /// <summary>
        /// Repeat string operation while equal (ZF=1) and CX not zero
        /// </summary>
        REPE,

        /// <summary>
        /// Repeat string operation while not equal (ZF=0) and CX not zero
        /// </summary>
        REPNE,

        /// <summary>
        /// Repeat string operation while not zero (ZF=0) and CX not zero
        /// </summary>
        REPNZ,

        /// <summary>
        /// Repeat string operation while zero (ZF=1) and CX not zero
        /// </summary>
        REPZ,

        /// <summary>
        /// Return from procedure
        /// </summary>
        RET,

        /// <summary>
        /// Return from far procedure
        /// </summary>
        RETF,

        /// <summary>
        /// Return from near procedure
        /// </summary>
        RETN,

        /// <summary>
        /// Rotate left
        /// </summary>
        ROL,

        /// <summary>
        /// Rotate right
        /// </summary>
        ROR,

        /// <summary>
        /// Store AH into FLAGS
        /// </summary>
        SAHF,

        /// <summary>
        /// Shift Arithmetically left (signed shift left)
        /// </summary>
        SAL,

        /// <summary>
        /// Shift Arithmetically right (signed shift right)
        /// </summary>
        SAR,

        /// <summary>
        /// Subtraction with borrow
        /// </summary>
        SBB,

        /// <summary>
        /// Compare byte string
        /// </summary>
        SCASB,

        /// <summary>
        /// Compare word string
        /// </summary>
        SCASW,

        /// <summary>
        /// Shift left (unsigned shift left)
        /// </summary>
        SHL,

        /// <summary>
        /// Shift right (unsigned shift right)
        /// </summary>
        SHR,

        /// <summary>
        /// Set carry flag
        /// </summary>
        STC,

        /// <summary>
        /// Set direction flag
        /// </summary>
        STD,

        /// <summary>
        /// Set interrupt flag
        /// </summary>
        STI,

        /// <summary>
        /// Store byte in string
        /// </summary>
        STOSB,

        /// <summary>
        /// Store word in string
        /// </summary>
        STOSW,

        /// <summary>
        /// Subtraction
        /// </summary>
        SUB,

        /// <summary>
        /// Logical compare (And)
        /// </summary>
        TEST,

        /// <summary>
        /// Wait until not busy
        /// </summary>
        WAIT,

        /// <summary>
        /// Exchange data
        /// </summary>
        XCHG,

        /// <summary>
        /// Table look-up translation
        /// </summary>
        XLAT,

        /// <summary>
        /// Exclusive Or
        /// </summary>
        XOR
    }
}
