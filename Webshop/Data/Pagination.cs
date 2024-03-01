using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Data;

// https://code-maze.com/paging-aspnet-core-webapi/
public class Pagination
{
	const int MaxSize = 20;

	private int page = 1;

	public int Page
	{
		get { return page; }
		set { page = value; }
	}

	private int size = 10;

	public int Size
	{
		get { return size; }
		set 
		{

			size = value > MaxSize ? MaxSize : value; 
		}
	}

	public int Skip() => (page - 1) * size;
}
