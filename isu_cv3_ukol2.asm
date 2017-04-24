;Vygenerujte pomocí instrukcí ADD a SUB pøíznaky: ZF,SF,CF,PF,AF. Použijte 16 bitové registry.
;Autor: T. Goldmann

%include "rw32-2017.inc"

section .data
    

section .text
main:
    mov ebp, esp
    
    ; write your code here
    mov ax,0
    
    ;ZF
    add ax,0
    
    ;PF
    add ax,3
    
    ;SF
    add ax,-4
    
    ;AF
    mov ax, 15
    add ax,1
    
    ;CF,OF
    mov ax, -0x8000
    add ax, -1

    xor eax, eax
    ret