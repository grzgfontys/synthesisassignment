using LogicLayer.DTOs;

namespace LogicLayer.InterfacesRepository
{
	public interface IItemCategoryRepository
	{
		bool CreateItemCategory(ItemCategoryDTO itemCategoryDTO);
		List<ItemCategoryDTO> ReadAllItemCategories();
		List<ItemCategoryDTO> ReadAllItemSubCategories();

        bool UpdateItemCategory(ItemCategoryDTO itemCategoryDTO);

		bool DeleteItemCategory(int Id);
	}
}
