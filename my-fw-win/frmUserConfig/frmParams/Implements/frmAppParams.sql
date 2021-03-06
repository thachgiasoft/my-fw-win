/******************************************************************************/
/***         Generated by IBExpert 2008.11.27 3/19/2009 11:25:33 AM         ***/
/******************************************************************************/



/******************************************************************************/
/***                                 Tables                                 ***/
/******************************************************************************/



CREATE TABLE FW_THAM_SO_UNG_DUNG (
    NHOM_THAM_SO      A_BIG_ID NOT NULL /* A_BIG_ID = BIGINT */,
    TEN_NHOM_THAM_SO  A_STR_LONG /* A_STR_LONG = VARCHAR(400) */,
    TEN_THAM_SO       A_STR_SHORT NOT NULL /* A_STR_SHORT = VARCHAR(100) */,
    TEN_THAM_SO_USER  A_STR_SHORT NOT NULL /* A_STR_SHORT = VARCHAR(100) */,
    GIA_TRI           A_STR_MEDIUM NOT NULL /* A_STR_MEDIUM = VARCHAR(200) */,
    MO_TA             A_STR_LONG /* A_STR_LONG = VARCHAR(400) */,
    DATA_TYPE         A_INTEGER NOT NULL /* A_INTEGER = INTEGER */,
    VISIBLE_BIT       A_BOOL_NULL /* A_BOOL_NULL = CHAR(1) */
);



/******************************************************************************/
/***                              Primary Keys                              ***/
/******************************************************************************/

ALTER TABLE FW_THAM_SO_UNG_DUNG ADD CONSTRAINT PK_FW_THAM_SO_UNG_DUNG PRIMARY KEY (TEN_THAM_SO);
