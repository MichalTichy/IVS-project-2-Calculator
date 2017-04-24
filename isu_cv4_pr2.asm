;Vytvoøte pole se ètyømi 16 bitovými prvky. Následnì vypoètìte výraz: x=pole[0]^2+pole[1]+pole[2]/(-2*pole[3])
;Autor: T. Goldmann

%include "rw32-2017.inc"

section .data
    pole dw 10,20,1000,40

section .text
main:
    mov ebp, esp
    ;call jednicka
    xor ebx,ebx
    xor ecx,ecx
    xor edx,edx
    mov ax, -200
    mov ebx, -20
    idiv ebx
    cwde 
    
    
    ret
    
    
    jednicka:
    mov EBX, 0xA1FF
    or EBX, 0x00FF
    ror EBX, 16
    mov ax, bx
    call WriteUInt16
    ret