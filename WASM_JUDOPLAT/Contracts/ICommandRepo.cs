﻿
namespace WASM_JUDOPLAT.Contracts
{
    public interface ICommandRepo:IBaseCRUD<CommandDto>
    {
        Task CallUpdate();
    }
}
