CREATE OR REPLACE PROCEDURE SBS_BUSCAR_USUARIO
(
P_ROLID IN SBS_USUARIO.ROLID%TYPE,
P_CODIGOUSUARIO IN SBS_USUARIO.CODIGOUSUARIO%TYPE,
IO_CURSOR OUT SYS_REFCURSOR
)
IS
BEGIN

  OPEN IO_CURSOR FOR
  
    SELECT
    usu.UsuarioId, usu.RolId, usu.CodigoUsuario,
    usu.PrimerNombre, usu.SegundoNombre, usu.ApellidoPaterno, usu.ApellidoMaterno, usu.Email
    FROM SBS_USUARIO usu
    WHERE
        (P_ROLID = 0 OR usu.ROLID = P_ROLID)
    AND (TRIM(P_CODIGOUSUARIO) = '' OR usu.CODIGOUSUARIO = P_CODIGOUSUARIO)
    AND usu.ACTIVO = 1
    ;

END;


/*
CREATE OR REPLACE PUBLIC SYNONYM SBS_BUSCAR_USUARIO
FOR GMATOS.SBS_BUSCAR_USUARIO;
*/


/*
select*from SBS_Usuario;

  declare
  p_ROLID CONSTANT SBS_USUARIO.ROLID%TYPE := 0;
  p_CODIGOUSUARIO CONSTANT SBS_USUARIO.CODIGOUSUARIO%TYPE := 'gmatos';
  v_IO_CURSOR SYS_REFCURSOR;
  v_USUARIOID SBS_USUARIO.USUARIOID%TYPE;
  v_ROLID SBS_USUARIO.ROLID%TYPE;
  v_CODIGOUSUARIO SBS_USUARIO.CODIGOUSUARIO%TYPE;
  v_PrimerNombre SBS_USUARIO.PRIMERNOMBRE%TYPE;
  v_SegundoNombre SBS_USUARIO.SEGUNDONOMBRE%TYPE;
  v_ApellidoPaterno SBS_USUARIO.APELLIDOPATERNO%TYPE;
  v_ApellidoMaterno SBS_USUARIO.APELLIDOMATERNO%TYPE;
  v_Email SBS_USUARIO.EMAIL%TYPE;

  begin
    
    SBS_BUSCAR_USUARIO(p_ROLID, p_CODIGOUSUARIO, v_IO_CURSOR);
    LOOP
      FETCH v_IO_CURSOR
      INTO v_USUARIOID, v_ROLID, v_CODIGOUSUARIO
      ,v_PrimerNombre, v_SegundoNombre, v_ApellidoPaterno, v_ApellidoMaterno, v_Email
      ;
      EXIT WHEN v_IO_CURSOR%NOTFOUND;
      DBMS_OUTPUT.PUT_LINE
      (
      v_USUARIOID || '     '  || v_ROLID || '     '  || v_CODIGOUSUARIO
      || '     '  || v_CODIGOUSUARIO
      || '     '  || v_PrimerNombre
      || '     '  || v_SegundoNombre
      || '     '  || v_ApellidoPaterno
      || '     '  || v_ApellidoMaterno
      || '     '  || v_Email
      );
    END LOOP;
    CLOSE v_IO_CURSOR;

  end;
*/
