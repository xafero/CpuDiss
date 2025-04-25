namespace ObjDumper.Core
{
    /// <summary>
    /// Instructions of the Hitachi SH-3 
    /// </summary>
    public enum Sh3
    {
        None = 0,

        /// <summary>
        /// Add Binary
        /// </summary>
        ADD,

        /// <summary>
        /// Add with Carry
        /// </summary>
        ADDC,

        /// <summary>
        /// Add with V Flag Overflow Check
        /// </summary>
        ADDV,

        /// <summary>
        /// And Logical
        /// </summary>
        AND,

        /// <summary>
        /// And Logical
        /// </summary>
        AND_B,

        /// <summary>
        /// Branch if False
        /// </summary>
        BF,

        /// <summary>
        /// Branch if False with Delay Slot
        /// </summary>
        BFs,

        /// <summary>
        /// Branch
        /// </summary>
        BRA,

        /// <summary>
        /// Branch Far
        /// </summary>
        BRAF,

        /// <summary>
        /// Branch to Subroutine
        /// </summary>
        BSR,

        /// <summary>
        /// Branch to Subroutine Far
        /// </summary>
        BSRF,

        /// <summary>
        /// Branch if True
        /// </summary>
        BT,

        /// <summary>
        /// Branch if True with Delay Slot
        /// </summary>
        BTs,

        /// <summary>
        /// Clear MAC Register
        /// </summary>
        CLRMAC,

        /// <summary>
        /// Clear S Bit
        /// </summary>
        CLRS,

        /// <summary>
        /// Clear T Bit
        /// </summary>
        CLRT,

        /// <summary>
        /// Compare Equal
        /// </summary>
        CMP_EQ,

        /// <summary>
        /// Compare Greater Than or Equal
        /// </summary>
        CMP_GE,

        /// <summary>
        /// Compare Greater Than
        /// </summary>
        CMP_GT,

        /// <summary>
        /// Compare Higher
        /// </summary>
        CMP_HI,

        /// <summary>
        /// Compare Higher or Same
        /// </summary>
        CMP_HS,

        /// <summary>
        /// Compare Plus
        /// </summary>
        CMP_PL,

        /// <summary>
        /// Compare Plus or Zero
        /// </summary>
        CMP_PZ,

        /// <summary>
        /// Compare String
        /// </summary>
        CMP_STR,

        /// <summary>
        /// Divide Step 0 as Signed
        /// </summary>
        DIV0S,

        /// <summary>
        /// Divide Step 0 as Unsigned
        /// </summary>
        DIV0U,

        /// <summary>
        /// Divide Step 1
        /// </summary>
        DIV1,

        /// <summary>
        /// Double-Length Multiply as Signed
        /// </summary>
        DMULS_L,

        /// <summary>
        /// Double-Length Multiply as Unsigned
        /// </summary>
        DMULU_L,

        /// <summary>
        /// Decrement and Test
        /// </summary>
        DT,

        /// <summary>
        /// Extend as Signed
        /// </summary>
        EXTS_B,

        /// <summary>
        /// Extend as Signed
        /// </summary>
        EXTS_W,

        /// <summary>
        /// Extend as Unsigned
        /// </summary>
        EXTU_B,

        /// <summary>
        /// Extend as Unsigned
        /// </summary>
        EXTU_W,

        /// <summary>
        /// Jump
        /// </summary>
        JMP,

        /// <summary>
        /// Jump to Subroutine
        /// </summary>
        JSR,

        /// <summary>
        /// Load to Control Register
        /// </summary>
        LDC,

        /// <summary>
        /// Load to Control Register
        /// </summary>
        LDC_L,

        /// <summary>
        /// Load to System Register
        /// </summary>
        LDS,

        /// <summary>
        /// Load to System Register
        /// </summary>
        LDS_L,

        /// <summary>
        /// Load PTEH/PTEL to TLB
        /// </summary>
        LDTLB,

        /// <summary>
        /// Multiply and Accumulate Long
        /// </summary>
        MAC_L,

        /// <summary>
        /// Multiply and Accumulate
        /// </summary>
        MAC_W,

        /// <summary>
        /// Move Data
        /// </summary>
        MOV,

        /// <summary>
        /// Move Data
        /// </summary>
        MOV_B,

        /// <summary>
        /// Move Data
        /// </summary>
        MOV_L,

        /// <summary>
        /// Move Data
        /// </summary>
        MOV_W,

        /// <summary>
        /// Move Effective Address
        /// </summary>
        MOVA,

        /// <summary>
        /// Move T Bit
        /// </summary>
        MOVT,

        /// <summary>
        /// Multiply Long
        /// </summary>
        MUL_L,

        /// <summary>
        /// Multiply as Signed Word
        /// </summary>
        MULS_W,

        /// <summary>
        /// Multiply as Unsigned Word
        /// </summary>
        MULU_W,

        /// <summary>
        /// Negate
        /// </summary>
        NEG,

        /// <summary>
        /// Negate with Carry
        /// </summary>
        NEGC,

        /// <summary>
        /// No Operation
        /// </summary>
        NOP,

        /// <summary>
        /// Not / Logical Complement
        /// </summary>
        NOT,

        /// <summary>
        /// Or Logical
        /// </summary>
        OR,

        /// <summary>
        /// Or Logical
        /// </summary>
        OR_B,

        /// <summary>
        /// Prefetch Data to the Cache
        /// </summary>
        PREF,

        /// <summary>
        /// Rotate with Carry Left
        /// </summary>
        ROTCL,

        /// <summary>
        /// Rotate with Carry Right
        /// </summary>
        ROTCR,

        /// <summary>
        /// Rotate Left
        /// </summary>
        ROTL,

        /// <summary>
        /// Rotate Right
        /// </summary>
        ROTR,

        /// <summary>
        /// Return from Exception
        /// </summary>
        RTE,

        /// <summary>
        /// Return from Subroutine
        /// </summary>
        RTS,

        /// <summary>
        /// Set S Bit
        /// </summary>
        SETS,

        /// <summary>
        /// Set T Bit
        /// </summary>
        SETT,

        /// <summary>
        /// Shift Arithmetic Dynamically
        /// </summary>
        SHAD,

        /// <summary>
        /// Shift Arithmetic Left
        /// </summary>
        SHAL,

        /// <summary>
        /// Shift Arithmetic Right
        /// </summary>
        SHAR,

        /// <summary>
        /// Shift Logical Dynamically
        /// </summary>
        SHLD,

        /// <summary>
        /// Shift Logical Left
        /// </summary>
        SHLL,

        /// <summary>
        /// Shift Logical Left 2 Bits
        /// </summary>
        SHLL2,

        /// <summary>
        /// Shift Logical Left 8 Bits
        /// </summary>
        SHLL8,

        /// <summary>
        /// Shift Logical Left 16 Bits
        /// </summary>
        SHLL16,

        /// <summary>
        /// Shift Logical Right
        /// </summary>
        SHLR,

        /// <summary>
        /// Shift Logical Right 2 Bits
        /// </summary>
        SHLR2,

        /// <summary>
        /// Shift Logical Right 8 Bits
        /// </summary>
        SHLR8,

        /// <summary>
        /// Shift Logical Right 16 Bits
        /// </summary>
        SHLR16,

        /// <summary>
        /// Sleep
        /// </summary>
        SLEEP,

        /// <summary>
        /// Store Control Register
        /// </summary>
        STC,

        /// <summary>
        /// Store Control Register
        /// </summary>
        STC_L,

        /// <summary>
        /// Store System Register
        /// </summary>
        STS,

        /// <summary>
        /// Store System Register
        /// </summary>
        STS_L,

        /// <summary>
        /// Subtract Binary
        /// </summary>
        SUB,

        /// <summary>
        /// Subtract with Carry
        /// </summary>
        SUBC,

        /// <summary>
        /// Subtract with V Flag Underflow Check
        /// </summary>
        SUBV,

        /// <summary>
        /// Swap Register Halves
        /// </summary>
        SWAP_B,

        /// <summary>
        /// Swap Register Halves
        /// </summary>
        SWAP_W,

        /// <summary>
        /// Test and Set
        /// </summary>
        TAS_B,

        /// <summary>
        /// Trap Always
        /// </summary>
        TRAPA,

        /// <summary>
        /// Test Logical
        /// </summary>
        TST,

        /// <summary>
        /// Test Logical
        /// </summary>
        TST_B,

        /// <summary>
        /// Exclusive Or Logical
        /// </summary>
        XOR,

        /// <summary>
        /// Exclusive Or Logical
        /// </summary>
        XOR_B,

        /// <summary>
        /// Extract
        /// </summary>
        XTRCT
    }
}
